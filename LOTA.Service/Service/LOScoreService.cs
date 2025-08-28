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

        public async Task<IEnumerable<CourseOfferingAssessmentDTO>> GetCourseOfferingsWithAssessmentsAsync()
        {
            // Get the latest trimester
            var latestTrimester = await _unitOfWork.trimesterRepository.GetLatestTrimestersAsync();
            if (latestTrimester == null)
            {
                throw new InvalidOperationException("No trimester information found");
            }

            // Get all course offerings in this trimester
            var courseOfferings = await _unitOfWork.trimesterCourseRepository.GetTrimesterCoursesByTrimesterAsync(latestTrimester.Id);

            var result = new List<CourseOfferingAssessmentDTO>();

            foreach (var courseOffering in courseOfferings)
            {
                // Get assessments for this course offering
                var assessments = await _unitOfWork.assessmentRepository.GetAssessmentsByCourseOfferingId(courseOffering.Id);
                
                // Get students enrolled in this course offering
                var students = await _unitOfWork.studentCourseRepository.GetByCourseOfferingIdAsync(courseOffering.Id);
                
                result.Add(new CourseOfferingAssessmentDTO
                {
                    TrimesterCourse = courseOffering,
                    Assessments = assessments.ToList(),
                    Students = students.ToList()
                });
            }

            return result;
        }

        public async Task<CourseOfferingAssessmentDTO> GetCourseOfferingWithAssessmentsAsync(string courseOfferingId)
        {
            var courseOffering = await _unitOfWork.trimesterCourseRepository.GetTrimesterCourseByIdAsync(courseOfferingId);
            if (courseOffering == null)
            {
                throw new InvalidOperationException("Course offering not found");
            }

            // Get assessments for this course offering
            var assessments = await _unitOfWork.assessmentRepository.GetAssessmentsByCourseOfferingId(courseOfferingId);

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
                IsRetake = studentAssessmentScore.IsRetake ?? false,
                RetakeDate = studentAssessmentScore.RetakeDate
            };
        }

        

        /// <summary>
        /// Batch save all LO scores for a student in a specific assessment
        /// </summary>
        public async Task<bool> BatchSaveStudentLOScoresAsync(string studentId, string assessmentId, List<LOScoreCreateDTO> loScores)
        {
            try
            {
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
                        IsRetake = false,
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

                        _unitOfWork.studentLOScoreRepository.Update(existingLOScore);
                    }
                }
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Batch Save Student LO Scores Failed");
            }
        }





        public async Task<bool> BatchSaveAllStudentsLOScoresAsync(AllStudentsLOScoresBatchSaveDTO batchSaveDTO)
        {
            try
            {
                foreach (var studentScore in batchSaveDTO.StudentScores)
                {
                    await BatchSaveStudentLOScoresAsync(
                        studentScore.StudentId, 
                        batchSaveDTO.AssessmentId, 
                        studentScore.LOScores);
                }

                await _unitOfWork.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Batch Save All Students LO Scores Failed: {ex.Message}");
            }
        }




    }
}
