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
        Task<bool> UpdateRetakeScoresAsync(RetakeRequestDTO retakeRequest);
        Task<List<FailedAssessmentForRetakeDTO>> GetFailedAssessmentsForRetakeAsync(string studentId, string courseOfferingId, string loName);
    }
}
