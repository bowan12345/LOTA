using LOTA.Model.DTO.Admin;
using LOTA.Model;

namespace LOTA.Service.Service.IService
{
    public interface ILOResultService
    {
        Task<IEnumerable<TrimesterCourse>> GetLatestTrimesterCourseOfferingsAsync();
        Task<LOResultDTO> GetLOResultsByCourseOfferingAsync(string courseOfferingId);
        Task UpdateRetakeScoresAsync(RetakeRequestDTO retakeRequest);
        Task<List<FailedAssessmentForRetakeDTO>> GetFailedAssessmentsForRetakeAsync(string studentId, string courseOfferingId, string loName);
    }
}
