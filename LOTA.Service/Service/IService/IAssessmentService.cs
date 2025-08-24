using LOTA.Model;
using LOTA.Model.DTO.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Service.Service.IService
{
    public interface IAssessmentService
    {
        Task<IEnumerable<AssessmentReturnDTO>> GetAllAssessmentsAsync();
        Task<AssessmentReturnDTO> GetAssessmentByIdAsync(string id);
        Task<IEnumerable<LearningOutcomeReturnDTO>> GetLearningOutcomesByCourseOfferingIdAsync(string courseOfferingId);
        Task<AssessmentReturnDTO> CreateAssessmentAsync(AssessmentCreateDTO assessmentCreateDTO);
        Task UpdateAssessmentAsync(Assessment assessment);
        Task DeleteAssessmentAsync(string id);
        Task<IEnumerable<AssessmentReturnDTO>> GetAssessmentsBySearchTermAsync(string searchTerm);
    }
}
