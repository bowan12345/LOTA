using LOTA.Service.Service.IService;
using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;
using LOTA.Model.DTO.Admin;
using Microsoft.EntityFrameworkCore;

namespace LOTA.Service.Service
{
    public class LOScoreService : ILOScoreService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LOScoreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CourseOfferingAssessmentDTO> GetCourseOfferingWithAssessmentsAsync(string courseOfferingId)
        {

            var courseOffering = await _unitOfWork.trimesterCourseRepository.GetTrimesterCourseByIdAsync(courseOfferingId);
            if (courseOffering == null)
            {
                throw new InvalidOperationException("Course offering not found");
            }

            // Get assessments for this course offering (order by creation time ascending)
            var assessments = await _unitOfWork.assessmentRepository.GetAssessmentsByCourseOfferingId(courseOfferingId);
            assessments = assessments
                .OrderBy(a => a.CreatedDate ?? DateTime.MinValue)
                .ToList();

            // Get students enrolled in this course offering
            var students = await _unitOfWork.studentCourseRepository.GetByCourseOfferingIdAsync(courseOfferingId);

            // Get all student assessment scores for this course offering
            var studentAssessmentScores = await _unitOfWork.studentScoreRepository.GetStudentScoresByCourseOfferingAsync(courseOfferingId);

            // Load StudentLOScores for each student assessment score
            foreach (var studentScore in studentAssessmentScores)
            {
                var loScores = await _unitOfWork.studentLOScoreRepository.GetStudentLOScoresByStudentAssessmentScoreAsync(studentScore.Id);
                // Load AssessmentLearningOutcome information for each LO score
                studentScore.StudentLOScores = loScores.ToList();
            }

            return new CourseOfferingAssessmentDTO
            {
                TrimesterCourse = courseOffering,
                Assessments = assessments.ToList(),
                StudentAssessmentScores = studentAssessmentScores.ToList(),
                Students = students.ToList()
            };
        }

        public async Task<LOScoreReturnDTO> GetLOScoreByIdAsync(string id)
        {
            var studentAssessmentScore = await _unitOfWork.studentScoreRepository.GetByIdAsync(id);

            if (studentAssessmentScore == null)
            {
                throw new InvalidOperationException("LO Score not found");
            }

            // Load related entities
            var assessment = await _unitOfWork.assessmentRepository.GetByIdAsync(studentAssessmentScore.AssessmentId);
            var student = await _unitOfWork.studentRepository.GetByIdAsync(studentAssessmentScore.StudentId);
            var courseOffering = await _unitOfWork.trimesterCourseRepository.GetTrimesterCourseByIdAsync(assessment?.CourseOfferingId);

            return new LOScoreReturnDTO
            {
                Id = studentAssessmentScore.Id,
            };
        }

        

        /// <summary>
        /// Batch save all LO scores for a student in a specific assessment
        /// </summary>
        public async Task BatchSaveStudentLOScoresAsync(string studentId, string assessmentId, List<LOScoreCreateDTO> loScores)
        {
            // Block editing if there exists any retake record under ANY assessment of the same course offering
            var assessmentEntity = await _unitOfWork.assessmentRepository.GetByIdAsync(assessmentId);
            if (assessmentEntity == null)
            {
                throw new InvalidOperationException($"Assessment not found");
            }
            var hasRetake = await _unitOfWork.studentLOScoreRepository.ExistsRetakeByCourseOfferingIdAsync(assessmentEntity.CourseOfferingId);
            if (hasRetake)
            {
                throw new InvalidOperationException("This course offering contains retake records and can no longer be edited.");
            }
           
            // Validate LO scores before saving
            await ValidateLOScoresAsync(assessmentId, loScores);
            
            // Check if StudentAssessmentScore record exists
            var existingStudentAssessmentScore = await _unitOfWork.studentScoreRepository.GetStudentScoreByStudentAssessmentAsync(
                studentId, assessmentId);

            string studentAssessmentScoreId;
            StudentAssessmentScore studentAssessmentScore;

            if (existingStudentAssessmentScore == null)
            {
                // Create new StudentAssessmentScore record with calculated total score
                var totalScore = loScores.Sum(s => s.Score);
                studentAssessmentScore = new StudentAssessmentScore
                {
                    Id = Guid.NewGuid().ToString(),
                    StudentId = studentId,
                    AssessmentId = assessmentId,
                    TotalScore = totalScore,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow
                };

                await _unitOfWork.studentScoreRepository.AddAsync(studentAssessmentScore);
                studentAssessmentScoreId = studentAssessmentScore.Id;
            }
            else
            {
                // Use existing record and update total score
                studentAssessmentScore = existingStudentAssessmentScore;
                studentAssessmentScoreId = existingStudentAssessmentScore.Id;
                
                // Calculate and update total score
                var totalScore = loScores.Sum(s => s.Score);
                studentAssessmentScore.TotalScore = totalScore;
                studentAssessmentScore.UpdatedDate = DateTime.UtcNow;
                _unitOfWork.studentScoreRepository.Update(studentAssessmentScore);
            }

            // Process each LO score
            foreach (var loScore in loScores)
            {
                // Check if StudentLOScore already exists
                var existingLOScore = await _unitOfWork.studentLOScoreRepository.GetStudentLOScoreByStudentAssessmentAndLOAsync(
                    studentAssessmentScoreId, loScore.AssessmentLearningOutcomeId);

                if (existingLOScore == null)
                {
                    // Create new StudentLOScore
                    var newLOScore = new StudentLOScore
                    {
                        Id = Guid.NewGuid().ToString(),
                        StudentAssessmentScoreId = studentAssessmentScoreId,
                        AssessmentLearningOutcomeId = loScore.AssessmentLearningOutcomeId,
                        Score = loScore.Score,
                        IsActive = true,
                        IsRetake = false,
                        CreatedDate = DateTime.UtcNow
                    };

                    await _unitOfWork.studentLOScoreRepository.AddAsync(newLOScore);
                }
                else
                {
                    // Update existing StudentLOScore
                    existingLOScore.Score = loScore.Score;
                    existingLOScore.UpdatedDate = DateTime.UtcNow;
                    existingLOScore.IsActive = true;
                    existingLOScore.IsRetake = false;

                    _unitOfWork.studentLOScoreRepository.Update(existingLOScore);
                }
            }
            await _unitOfWork.SaveAsync();
        }



        private async Task<bool> ValidateLOScoresAsync(string assessmentId, List<LOScoreCreateDTO> loScores)
        {
                // Get the assessment with its learning outcomes
                var assessment = await _unitOfWork.assessmentRepository.GetByIdAsync(assessmentId);
                if (assessment == null)
                {
                    throw new InvalidOperationException($"Assessment {assessmentId} not found");
                }

                // Get all assessment learning outcomes for this assessment
                var assessmentLearningOutcomes = await _unitOfWork.assessmentRepository.GetLOListByAssessmentId(assessmentId);
                
                foreach (var loScore in loScores)
                {
                    // Find the corresponding assessment learning outcome
                    var assessmentLO = assessmentLearningOutcomes.FirstOrDefault(alo => alo.Id == loScore.AssessmentLearningOutcomeId);
                    if (assessmentLO == null)
                    {
                        throw new InvalidOperationException($"Assessment Learning Outcome not found for Assessment");
                    }

                    // Validate score range
                    if (loScore.Score < 0)
                    {
                        throw new InvalidOperationException($"Score cannot be negative.");
                    }

                    if (loScore.Score > assessmentLO.Score)
                    {
                        throw new InvalidOperationException($"Score cannot exceed maximum LO score.");
                    }
                }

                return true;
        }


        public async Task BatchSaveAllStudentsLOScoresAsync(AllStudentsLOScoresBatchSaveDTO batchSaveDTO)
        {
            // Block editing if there exists any retake record under ANY assessment of the same course offering
            var assessmentEntity = await _unitOfWork.assessmentRepository.GetByIdAsync(batchSaveDTO.AssessmentId);
            if (assessmentEntity == null)
            {
                throw new InvalidOperationException($"Assessment not found");
            }
            var hasRetake = await _unitOfWork.studentLOScoreRepository.ExistsRetakeByCourseOfferingIdAsync(assessmentEntity.CourseOfferingId);
            if (hasRetake)
            {
                throw new InvalidOperationException("This course offering contains retake records and can no longer be edited.");
            }
            // Validate all LO scores before saving
            foreach (var studentScore in batchSaveDTO.StudentScores)
            {
                await ValidateLOScoresAsync(batchSaveDTO.AssessmentId, studentScore.LOScores);
            }
            
            foreach (var studentScore in batchSaveDTO.StudentScores)
            {
                await BatchSaveStudentLOScoresAsync(
                    studentScore.StudentId, 
                    batchSaveDTO.AssessmentId, 
                    studentScore.LOScores);
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task<CourseOfferingDetailsDTO> GetCourseOfferingDetailsByCourseOfferingId(string courseOfferingId)
        {
            // Get only necessary information: Course Offering, Trimester, Assessments
            var courseOffering = await _unitOfWork.trimesterCourseRepository.GetTrimesterCourseByIdAsync(courseOfferingId);
            if (courseOffering == null)
            {
                throw new InvalidOperationException("Course offering not found");
            }

            // Get assessments for this course offering (order by creation time ascending)
            var assessments = await _unitOfWork.assessmentRepository.GetAssessmentsByCourseOfferingId(courseOfferingId);
            assessments = assessments
                .OrderBy(a => a.CreatedDate ?? DateTime.MinValue)
                .ToList();
            
            // Create a simplified DTO with only necessary information
            var courseOfferingDto = new CourseOfferingDetailsDTO
            {
                TrimesterCourse = new TrimesterCourseInfo
                {
                    Id = courseOffering.Id,
                    Course = new CourseInfoDTO
                    {
                        Id = courseOffering.Course?.Id,
                        CourseName = courseOffering.Course?.CourseName,
                        CourseCode = courseOffering.Course?.CourseCode,
                        Description = courseOffering.Course?.Description,
                        Credits = 0 // Credits property doesn't exist in Course entity
                    },
                    Trimester = new TrimesterInfoDTO
                    {
                        Id = courseOffering.Trimester?.Id,
                        TrimesterNumber = courseOffering.Trimester?.TrimesterNumber ?? 0,
                        AcademicYear = courseOffering.Trimester?.AcademicYear ?? 0,
                        StartDate = courseOffering.Trimester?.CreatedDate ?? DateTime.MinValue, 
                        EndDate = courseOffering.Trimester?.UpdatedDate ?? DateTime.MinValue 
                    }
                },
                Assessments = assessments?.Select(a => new AssessmentInfoDTO
                {
                    Id = a.Id,
                    AssessmentName = a.AssessmentName,
                    Description = a.AssessmentName, 
                    Weight = a.Weight,
                    Score = a.Score,
                    DueDate = a.CreatedDate ?? DateTime.MinValue, 
                    AssessmentType = new AssessmentTypeInfoDTO
                    {
                        Id = a.AssessmentType?.Id,
                        AssessmentTypeName = a.AssessmentType?.AssessmentTypeName,
                        Description = a.AssessmentType?.AssessmentTypeName 
                    }
                }).ToList() ?? new List<AssessmentInfoDTO>()
            };
            
            return courseOfferingDto;
        }

        public async Task<IEnumerable<TrimesterCourse>> GetLatestTrimesterCourseOfferingsAsync()
        {
            try
            {
                return await _unitOfWork.trimesterCourseRepository.GetLatestTrimesterCourseOfferingsAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to get latest trimester course offerings");
            }
        }
    }
}
