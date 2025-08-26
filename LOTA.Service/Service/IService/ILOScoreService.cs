using LOTA.Model.DTO.Admin;

namespace LOTA.Service.Service.IService
{
    public interface ILOScoreService
    {
        Task<IEnumerable<CourseOfferingAssessmentDTO>> GetCourseOfferingsWithAssessmentsAsync();
        Task<CourseOfferingAssessmentDTO> GetCourseOfferingWithAssessmentsAsync(string courseOfferingId);
        Task<LOScoreReturnDTO> GetLOScoreByIdAsync(string id);
        Task<LOScoreReturnDTO> CreateLOScoreAsync(LOScoreCreateDTO loscoreCreateDTO);
        Task<LOScoreReturnDTO> UpdateLOScoreAsync(LOScoreUpdateDTO loscoreUpdateDTO);
        Task<bool> DeleteLOScoreAsync(string id);
        Task<IEnumerable<LOScoreReturnDTO>> GetLOScoresByAssessmentAsync(string assessmentId);
        Task<IEnumerable<LOScoreReturnDTO>> GetLOScoresByStudentAsync(string studentId);
        Task<IEnumerable<LOScoreReturnDTO>> GetLOScoresByCourseOfferingAsync(string courseOfferingId);
    }
}
