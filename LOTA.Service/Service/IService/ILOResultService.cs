using LOTA.Model.DTO.Admin;
using LOTA.Model;

namespace LOTA.Service.Service.IService
{
    public interface ILOResultService
    {
        Task<List<LOResultDTO>> GetLOResultsByQualificationAsync();
        Task<IEnumerable<TrimesterCourse>> GetLatestTrimesterCourseOfferingsAsync();
        Task<LOResultDTO> GetLOResultsByCourseOfferingAsync(string courseOfferingId);
        Task<StudentResultDTO> GetStudentLOResultAsync(string studentId, string courseOfferingId);
        Task<bool> UpdateStudentLOScoreAsync(string studentId, string assessmentId, string learningOutcomeId, decimal score);
        Task<bool> UpdateRetakeScoresAsync(RetakeScoreDTO retakeScoreDTO);
        Task<List<RetakeHistoryDTO>> GetRetakeHistoryAsync(string studentId, string learningOutcomeName);
        Task<List<object>> GetFailedAssessmentsForRetakeAsync(string studentId, string courseOfferingId, string loName);
        Task<object> GetRetakeDataByAssessmentIdsAsync(string studentId, string courseOfferingId, List<string> assessmentIds);
    }
}
