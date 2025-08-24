using LOTA.Service.Service.IService;
using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;
using LOTA.Model.DTO.Admin;
using Microsoft.EntityFrameworkCore;

namespace LOTA.Service.Service
{
    public class AssessmentService : IAssessmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AssessmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AssessmentReturnDTO>> GetAllAssessmentsAsync()
        {
            // Load assessments with navigation properties
            var assessments = await _unitOfWork.assessmentRepository.GetAllAsync(includeProperties: "AssessmentType,TrimesterCourse,TrimesterCourse.Course,Trimester");
            
            var assessmentsWithDetails = new List<AssessmentReturnDTO>();
            
            foreach (var assessment in assessments)
            {
                assessmentsWithDetails.Add(MapToReturnDTO(assessment, assessment.TrimesterCourse, assessment.Trimester, assessment.AssessmentType));
            }
            
            return assessmentsWithDetails;
        }

        public async Task<AssessmentReturnDTO> GetAssessmentByIdAsync(string id)
        {
            var assessment = await _unitOfWork.assessmentRepository.GetByIdAsync(id, includeProperties: "AssessmentType,TrimesterCourse,TrimesterCourse.Course,Trimester");
            if (assessment == null) return null;
            
            return MapToReturnDTO(assessment, assessment.TrimesterCourse, assessment.Trimester, assessment.AssessmentType);
        }

        public async Task<AssessmentReturnDTO> CreateAssessmentAsync(AssessmentCreateDTO assessmentCreateDTO)
        {
            try
            {
                // Create new assessment
                var assessment = new Assessment
                {
                    Id = Guid.NewGuid().ToString(),
                    AssessmentName = assessmentCreateDTO.AssessmentName,
                    AssessmentTypeId = assessmentCreateDTO.AssessmentTypeId,
                    TotalWeight = assessmentCreateDTO.TotalWeight,
                    TotalScore = assessmentCreateDTO.TotalScore,
                    CourseOfferingId = assessmentCreateDTO.CourseOfferingId,
                    TrimesterId = assessmentCreateDTO.TrimesterId,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow,
                };
                Assessment createAssessment = await _unitOfWork.assessmentRepository.AddAsync(assessment);
                await _unitOfWork.SaveAsync();
                return new AssessmentReturnDTO()
                {
                    Id = createAssessment.Id,
                    AssessmentName = createAssessment.AssessmentName,
                    AssessmentType = createAssessment.AssessmentType,
                    TotalWeight = createAssessment.TotalWeight,
                    TotalScore = createAssessment.TotalScore
                };
            }
            catch(Exception ex)
            {
                throw new NotImplementedException("Create assessment is failed");
            }
        }

        public async Task UpdateAssessmentAsync(Assessment assessment)
        {
            try
            {
                var assessmentDb = await _unitOfWork.assessmentRepository.GetByIdAsync(assessment.Id);
                if (assessmentDb == null)
                {
                    throw new NotImplementedException("Assessment is not found");
                }
                _unitOfWork.assessmentRepository.Update(assessment);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Update assessment is failed");
            }
        }

        public async Task DeleteAssessmentAsync(string id)
        {
            try
            {
                var assessment = await _unitOfWork.assessmentRepository.GetByIdAsync(id);
                if (assessment == null)
                {
                    throw new NotImplementedException("Assessment is not found");
                }
                _unitOfWork.assessmentRepository.Remove(id);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Delete assessment is failed");
            }
        }

        public async Task<IEnumerable<AssessmentReturnDTO>> GetAssessmentsBySearchTermAsync(string searchTerm)
        {
            // Load assessments with navigation properties
            var assessments = await _unitOfWork.assessmentRepository.GetAllAsync(includeProperties: "AssessmentType,TrimesterCourse,TrimesterCourse.Course,Trimester");
            
            var filteredAssessments = assessments.Where(a => 
                a.AssessmentName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                (a.TrimesterCourse != null && a.TrimesterCourse.Course.CourseName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                (a.TrimesterCourse != null && a.TrimesterCourse.Course.CourseCode.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            );
            
            var assessmentsWithDetails = new List<AssessmentReturnDTO>();
            
            foreach (var assessment in filteredAssessments)
            {
                assessmentsWithDetails.Add(MapToReturnDTO(assessment, assessment.TrimesterCourse, assessment.Trimester, assessment.AssessmentType));
            }
            
            return assessmentsWithDetails;
        }

        private AssessmentReturnDTO MapToReturnDTO(Assessment assessment, TrimesterCourse trimesterCourse = null, Trimester trimester = null, AssessmentType assessmentType = null)
        {
            return new AssessmentReturnDTO
            {
                Id = assessment.Id,
                AssessmentName = assessment.AssessmentName,
                AssessmentType = assessmentType ?? assessment.AssessmentType,
                TotalWeight = assessment.TotalWeight,
                TotalScore = assessment.TotalScore,
                IsActive = assessment.IsActive,
                CreatedBy = assessment.CreatedBy,
                CreatedDate = assessment.CreatedDate,
                UpdatedDate = assessment.UpdatedDate,
                TrimesterCourse = trimesterCourse ?? assessment.TrimesterCourse,
                Trimester = assessment.Trimester,
                AssessmentLearningOutcomes = assessment.AssessmentLearningOutcomes
            };
        }

        public async Task<IEnumerable<LearningOutcomeReturnDTO>> GetLearningOutcomesByCourseOfferingIdAsync(string courseOfferingId)
        {
            ICollection<LearningOutcomeReturnDTO> results = new List<LearningOutcomeReturnDTO>();
            var trimesterCourse= await _unitOfWork.trimesterCourseRepository.GetTrimesterCourseByIdAsync(courseOfferingId);
            if (trimesterCourse == null)
            {
                throw new NotImplementedException("Course Offering not found");
            }
            // query los for course
            ICollection <LearningOutcome> learningOutcomes = trimesterCourse.Course.LearningOutcomes;
            foreach (var lo in learningOutcomes)
            {
                results.Add(new LearningOutcomeReturnDTO
                {
                    Id = lo.Id,
                    LOName = lo.LOName,
                    Description = lo.Description,
                });
                
            }
            return results;
        }


      
    }
}
