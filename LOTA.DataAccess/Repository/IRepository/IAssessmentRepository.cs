using LOTA.Model;
using LOTA.Model.DTO.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.DataAccess.Repository.IRepository
{
    public interface IAssessmentRepository : IRepository<Assessment, string>
    {
        Task AddLearningOutcomesAsync(List<AssessmentLearningOutcome> learningOutcomes);
        Task<IEnumerable<Assessment>> GetAssessmentsByCourseOfferingId(string? courseOfferingId);
        Task<IEnumerable<AssessmentLearningOutcome>> GetLOListByAssessmentId(string? assessmentId, string? includeProperties = null);
        void RemoveLearningOutcomesByAssessmentIdAsync(string? assessmentId);
        void RemoveLearningOutcomeById(string? learningOutcomeId);
        void UpdateAssessmentLearningOutcome(AssessmentLearningOutcome learningOutcome);
        Task<IEnumerable<AssessmentWithLOsDTO>> GetAssessmentsWithLOsByCourseOfferingId(string courseOfferingId);
        void RemoveAssessmentsByCourseOfferingId(string courseOfferingId);
    }
}
