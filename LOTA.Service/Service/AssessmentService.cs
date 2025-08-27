using LOTA.Service.Service.IService;
using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;
using LOTA.Model.DTO.Admin;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;
using Azure.Core;

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
            //get latest trimester
            var trimester = await _unitOfWork.trimesterRepository.GetLatestTrimestersAsync();
            if (trimester == null)
            {
                throw new NotImplementedException("No trimester Information ");
            }
            // get assessments at latest trimester
            var assessments = await _unitOfWork.assessmentRepository.GetAllAsync(a=>a.TrimesterId == trimester.Id,includeProperties: "AssessmentType,TrimesterCourse,TrimesterCourse.Course,Trimester");
            var assessmentsWithDetails = new List<AssessmentReturnDTO>();
            
            foreach (var assessment in assessments)
            {
                assessmentsWithDetails.Add(MapToReturnDTO(assessment, assessment.TrimesterCourse, assessment.Trimester, assessment.AssessmentType));
            }
            
            return assessmentsWithDetails;
        }

        public async Task<AssessmentReturnDTO> GetAssessmentByIdAsync(string id)
        {
            //get assessment info
            var assessment = await _unitOfWork.assessmentRepository.GetByIdAsync(id, includeProperties: "AssessmentType,TrimesterCourse,TrimesterCourse.Course.LearningOutcomes");
            if (assessment == null)
            {
                throw new NotImplementedException("Assessment information has not found");
            }
            //check LO info
            IEnumerable<AssessmentLearningOutcome> LoLists = await _unitOfWork.assessmentRepository.GetLOListByAssessmentId(assessment.Id, "LearningOutcome");
            if (LoLists == null || LoLists.Count()<=0)
            {
                throw new NotImplementedException("Learning outcome information has not found");
            }
            assessment.AssessmentLearningOutcomes = LoLists.ToList();
            return MapToReturnDTO(assessment, assessment.TrimesterCourse, assessment.Trimester, assessment.AssessmentType);
        }

        public async Task<AssessmentReturnDTO> CreateAssessmentAsync(AssessmentCreateDTO assessmentCreateDTO)
        {
            //checks if LOs are empty or and sum of LOs equals assessment's score
            List<AssessmentLearningOutcomeCreateDTO> learningOutcomes = assessmentCreateDTO.LearningOutcomes;
            if (learningOutcomes.Sum(lo => lo.Score) != assessmentCreateDTO.Score)
            {
                throw new NotImplementedException("The sum of the LO's score should equal the assessment's score");
            }
            //checks if it has assessments under the offered course
            IEnumerable<Assessment> courseofferedassessmentList = await _unitOfWork.assessmentRepository.GetAssessmentsByCourseOfferingId(assessmentCreateDTO.CourseOfferingId);
            if (courseofferedassessmentList != null && courseofferedassessmentList.Count() > 0)
            {
                //calculate total weight of all assessments of this offered course 
                decimal totalWeight = courseofferedassessmentList.Sum(a => a.Weight);
                if (totalWeight + assessmentCreateDTO.Weight > 100m)
                {
                    throw new NotImplementedException("Total Weight of This Course Cannot exceed 100%");
                }
            }
            // Create new assessment
            var assessment = new Assessment
            {
                Id = Guid.NewGuid().ToString(),
                AssessmentName = assessmentCreateDTO.AssessmentName,
                AssessmentTypeId = assessmentCreateDTO.AssessmentTypeId,
                Weight = assessmentCreateDTO.Weight,
                Score = assessmentCreateDTO.Score,
                CourseOfferingId = assessmentCreateDTO.CourseOfferingId,
                TrimesterId = assessmentCreateDTO.TrimesterId,
                IsActive = true,
                CreatedDate = DateTime.UtcNow,
            };
            //add new assessment
            Assessment createAssessment = await _unitOfWork.assessmentRepository.AddAsync(assessment);
            //add LOs of the assessment
            var LOList = assessmentCreateDTO.LearningOutcomes.Select(lo => new AssessmentLearningOutcome
            {
                Id = Guid.NewGuid().ToString(),
                AssessmentId = createAssessment.Id,
                LOId = lo.LOId,
                Score = lo.Score
            }).ToList();
            await _unitOfWork.assessmentRepository.AddLearningOutcomesAsync(LOList);
            //save to database
            await _unitOfWork.SaveAsync();
            return new AssessmentReturnDTO()
            {
                Id = createAssessment.Id,
                AssessmentName = createAssessment.AssessmentName,
                AssessmentType = createAssessment.AssessmentType,
                Weight = createAssessment.Weight,
                Score = createAssessment.Score
            };
        }
           
        public async Task UpdateAssessmentAsync(AssessmentUpdateDTO assessmentDTO)
        {
            //checks if LOs are empty or and sum of LOs equals assessment's score
            List<AssessmentLearningOutcomeCreateDTO> learningOutcomes = assessmentDTO.LearningOutcomes;
            if (learningOutcomes.Sum(lo => lo.Score) != assessmentDTO.Score)
            {
                throw new NotImplementedException("The sum of the LO's score should equal the assessment's score");
            }

            //checks if it has assessments under the offered course
            IEnumerable<Assessment> courseofferedassessmentList = await _unitOfWork.assessmentRepository.GetAssessmentsByCourseOfferingId(assessmentDTO.CourseOfferingId);
            if (courseofferedassessmentList != null && courseofferedassessmentList.Count() > 0)
            {
                List<Assessment> assessments = courseofferedassessmentList.Where(a => a.Id != assessmentDTO.Id).ToList();
                //calculate total weight of all assessments of this offered course 
                decimal totalWeight = assessments.Sum(a => a.Weight);
                if (totalWeight + assessmentDTO.Weight > 100m)
                {
                    throw new NotImplementedException("Total Weight of This Course Cannot exceed 100%");
                }
            }
            var assessmentDb = await _unitOfWork.assessmentRepository.GetByIdAsync(assessmentDTO.Id);
            if (assessmentDb == null)
            {
                throw new NotImplementedException("Assessment is not found");
            }
            // Update assessment
            assessmentDb.AssessmentName = assessmentDTO.AssessmentName;
            assessmentDb.AssessmentTypeId = assessmentDTO.AssessmentTypeId;
            assessmentDb.Weight = assessmentDTO.Weight;
            assessmentDb.Score = assessmentDTO.Score;
            assessmentDb.UpdatedDate = DateTime.UtcNow;

            //remove old LOs of the assessment
            _unitOfWork.assessmentRepository.RemoveLearningOutcomesByAssessmentIdAsync(assessmentDb.Id);
            //add new LOs of the assessment
            var LOList = assessmentDTO.LearningOutcomes.Select(lo => new AssessmentLearningOutcome
            {
                Id = Guid.NewGuid().ToString(),
                AssessmentId = assessmentDTO.Id,
                LOId = lo.LOId,
                Score = lo.Score
            }).ToList();
            await _unitOfWork.assessmentRepository.AddLearningOutcomesAsync(LOList);
            //save to database
            await _unitOfWork.SaveAsync();

        }

        public async Task DeleteAssessmentAsync(string id)
        {
            var assessment = await _unitOfWork.assessmentRepository.GetByIdAsync(id);
            if (assessment == null)
            {
                throw new NotImplementedException("Assessment is not found");
            }
            _unitOfWork.assessmentRepository.Remove(id);
            await _unitOfWork.SaveAsync();
           
        }

        public async Task<IEnumerable<AssessmentReturnDTO>> GetAssessmentsBySearchTermAsync(string searchTerm)
        {
            //get latest trimester
            var trimester = await _unitOfWork.trimesterRepository.GetLatestTrimestersAsync();
            if (trimester == null)
            {
                throw new NotImplementedException("No trimester Information ");
            }
            // get assessments at latest trimester
            var assessments = await _unitOfWork.assessmentRepository.GetAllAsync(a=>a.TrimesterId == trimester.Id, includeProperties: "AssessmentType,TrimesterCourse,TrimesterCourse.Course,Trimester");
            var filteredAssessments = assessments.Where(a => 
                 a.TrimesterCourse != null && a.TrimesterCourse.Course.CourseName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
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
                Weight = assessment.Weight,
                Score = assessment.Score,
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
