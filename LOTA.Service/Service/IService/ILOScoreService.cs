using LOTA.Model;
using LOTA.Model.DTO.Admin;

namespace LOTA.Service.Service.IService
{
    public interface ILOScoreService
    {
        Task<IEnumerable<TrimesterCourse>> GetLatestTrimesterCourseOfferingsAsync();
        Task<IEnumerable<CourseOfferingAssessmentDTO>> GetCourseOfferingsWithAssessmentsAsync();
        Task<CourseOfferingAssessmentDTO> GetCourseOfferingWithAssessmentsAsync(string courseOfferingId);
        Task BatchSaveStudentLOScoresAsync(string studentId, string assessmentId, List<LOScoreCreateDTO> loScores);
        Task BatchSaveAllStudentsLOScoresAsync(AllStudentsLOScoresBatchSaveDTO batchSaveDTO);
    }
}
