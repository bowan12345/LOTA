using LOTA.Model;
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
    }
}
