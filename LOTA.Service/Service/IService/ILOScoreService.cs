using LOTA.Model.DTO.Admin;

namespace LOTA.Service.Service.IService
{
    public interface ILOScoreService
    {
        Task<IEnumerable<CourseOfferingAssessmentDTO>> GetCourseOfferingsWithAssessmentsAsync();
        Task<CourseOfferingAssessmentDTO> GetCourseOfferingWithAssessmentsAsync(string courseOfferingId);
        Task<LOScoreReturnDTO> GetLOScoreByIdAsync(string id);
        Task BatchSaveStudentLOScoresAsync(string studentId, string assessmentId, List<LOScoreCreateDTO> loScores);
        Task BatchSaveAllStudentsLOScoresAsync(AllStudentsLOScoresBatchSaveDTO batchSaveDTO);
        Task<bool> ValidateLOScoresAsync(string assessmentId, List<LOScoreCreateDTO> loScores);
    }
}
