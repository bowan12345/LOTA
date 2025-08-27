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

            return new CourseOfferingAssessmentDTO
            {
                TrimesterCourse = courseOffering,
                Assessments = assessments.ToList(),
                Students = students.ToList()
            };
        }

        public async Task<LOScoreReturnDTO> GetLOScoreByIdAsync(string id)
        {
            var studentScore = await _unitOfWork.studentScoreRepository.GetByIdAsync(id);

            if (studentScore == null)
            {
                throw new InvalidOperationException("LO Score not found");
            }

            // Load related entities
            var assessment = await _unitOfWork.assessmentRepository.GetByIdAsync(studentScore.AssessmentId);
            var student = await _unitOfWork.studentRepository.GetByIdAsync(studentScore.StudentId);
            var learningOutcome = await _unitOfWork.learningOutcomeRepository.GetByIdAsync(studentScore.LOId);
            var trimester = await _unitOfWork.trimesterRepository.GetByIdAsync(studentScore.TrimesterId);
            var courseOffering = await _unitOfWork.trimesterCourseRepository.GetTrimesterCourseByIdAsync(assessment?.CourseOfferingId);

            return new LOScoreReturnDTO
            {
                Id = studentScore.Id,
                IsRetake = studentScore.IsRetake ?? false,
                RetakeDate = studentScore.RetakeDate
            };
        }

        public async Task<LOScoreReturnDTO> CreateLOScoreAsync(LOScoreCreateDTO loscoreCreateDTO)
        {
            // Check if a record already exists
            var existingScore = await _unitOfWork.studentScoreRepository.GetStudentScoreByStudentAssessmentLOAsync(
                loscoreCreateDTO.StudentId,
                loscoreCreateDTO.AssessmentId,
                loscoreCreateDTO.LOId
            );

            if (existingScore != null)
            {
                throw new InvalidOperationException("Score for this student, assessment, and LO already exists");
            }

            var studentScore = new StudentAssessmentScore
            {
                Id = Guid.NewGuid().ToString(),
                StudentId = loscoreCreateDTO.StudentId,
                AssessmentId = loscoreCreateDTO.AssessmentId,
                LOId = loscoreCreateDTO.LOId,
                TrimesterId = loscoreCreateDTO.TrimesterId,
                TotalScore = loscoreCreateDTO.Score,
                IsActive = true,
                IsRetake = loscoreCreateDTO.IsRetake,
                RetakeDate = loscoreCreateDTO.RetakeDate,
                CreatedDate = DateTime.UtcNow
            };

            await _unitOfWork.studentScoreRepository.AddAsync(studentScore);
            await _unitOfWork.SaveAsync();

            return await GetLOScoreByIdAsync(studentScore.Id);
        }

        public async Task<LOScoreReturnDTO> UpdateLOScoreAsync(LOScoreUpdateDTO loscoreUpdateDTO)
        {
            var studentScore = await _unitOfWork.studentScoreRepository.GetByIdAsync(loscoreUpdateDTO.Id);
            if (studentScore == null)
            {
                throw new InvalidOperationException("LO Score not found");
            }

            studentScore.TotalScore = loscoreUpdateDTO.Score;
            studentScore.IsActive = true;
            studentScore.IsRetake = loscoreUpdateDTO.IsRetake;
            studentScore.RetakeDate = loscoreUpdateDTO.RetakeDate;
            studentScore.UpdatedDate = DateTime.UtcNow;

            _unitOfWork.studentScoreRepository.Update(studentScore);
            await _unitOfWork.SaveAsync();

            return await GetLOScoreByIdAsync(studentScore.Id);
        }

        public async Task<bool> DeleteLOScoreAsync(string id)
        {
            var studentScore = await _unitOfWork.studentScoreRepository.GetByIdAsync(id);
            if (studentScore == null)
            {
                return false;
            }

            _unitOfWork.studentScoreRepository.Remove(studentScore.Id);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<LOScoreReturnDTO>> GetLOScoresByAssessmentAsync(string assessmentId)
        {
            var studentScores = await _unitOfWork.studentScoreRepository.GetStudentScoresByAssessmentAsync(assessmentId);
            var result = new List<LOScoreReturnDTO>();

            foreach (var ss in studentScores)
            {
                var assessment = await _unitOfWork.assessmentRepository.GetByIdAsync(ss.AssessmentId);
                var student = await _unitOfWork.studentRepository.GetByIdAsync(ss.StudentId);
                var learningOutcome = await _unitOfWork.learningOutcomeRepository.GetByIdAsync(ss.LOId);
                var trimester = await _unitOfWork.trimesterRepository.GetByIdAsync(ss.TrimesterId);
                var courseOffering = await _unitOfWork.trimesterCourseRepository.GetTrimesterCourseByIdAsync(assessment?.CourseOfferingId);

                result.Add(new LOScoreReturnDTO
                {
                    Id = ss.Id,
                    IsRetake = ss.IsRetake ?? false,
                    RetakeDate = ss.RetakeDate
                });
            }

            return result;
        }

        public async Task<IEnumerable<LOScoreReturnDTO>> GetLOScoresByStudentAsync(string studentId)
        {
            var studentScores = await _unitOfWork.studentScoreRepository.GetStudentScoresByStudentAsync(studentId);
            var result = new List<LOScoreReturnDTO>();

            foreach (var ss in studentScores)
            {
                var assessment = await _unitOfWork.assessmentRepository.GetByIdAsync(ss.AssessmentId);
                var student = await _unitOfWork.studentRepository.GetByIdAsync(ss.StudentId);
                var learningOutcome = await _unitOfWork.learningOutcomeRepository.GetByIdAsync(ss.LOId);
                var trimester = await _unitOfWork.trimesterRepository.GetByIdAsync(ss.TrimesterId);
                var courseOffering = await _unitOfWork.trimesterCourseRepository.GetTrimesterCourseByIdAsync(assessment?.CourseOfferingId);

                result.Add(new LOScoreReturnDTO
                {
                    Id = ss.Id,
                    IsRetake = ss.IsRetake ?? false,
                    RetakeDate = ss.RetakeDate
                });
            }

            return result;
        }

        public async Task<IEnumerable<LOScoreReturnDTO>> GetLOScoresByCourseOfferingAsync(string courseOfferingId)
        {
            var studentScores = await _unitOfWork.studentScoreRepository.GetStudentScoresByCourseOfferingAsync(courseOfferingId);
            var result = new List<LOScoreReturnDTO>();

            foreach (var ss in studentScores)
            {
                var assessment = await _unitOfWork.assessmentRepository.GetByIdAsync(ss.AssessmentId);
                var student = await _unitOfWork.studentRepository.GetByIdAsync(ss.StudentId);
                var learningOutcome = await _unitOfWork.learningOutcomeRepository.GetByIdAsync(ss.LOId);
                var trimester = await _unitOfWork.trimesterRepository.GetByIdAsync(ss.TrimesterId);
                var courseOffering = await _unitOfWork.trimesterCourseRepository.GetTrimesterCourseByIdAsync(assessment?.CourseOfferingId);

                result.Add(new LOScoreReturnDTO
                {
                    Id = ss.Id,
                    IsRetake = ss.IsRetake ?? false,
                    RetakeDate = ss.RetakeDate
                });
            }

            return result;
        }
    }
}
