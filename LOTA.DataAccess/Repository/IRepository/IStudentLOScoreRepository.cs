using LOTA.Model;

namespace LOTA.DataAccess.Repository.IRepository
{
    public interface IStudentLOScoreRepository : IRepository<StudentLOScore, string>
    {
        Task<IEnumerable<StudentLOScore>> GetStudentLOScoresByStudentAssessmentScoreAsync(string studentAssessmentScoreId);
        Task<StudentLOScore> GetStudentLOScoreByStudentAssessmentScoreAndLOAsync(string studentAssessmentScoreId, string loId);
        Task<StudentLOScore> GetStudentLOScoreByStudentAssessmentAndLOAsync(string studentAssessmentScoreId, string assessmentLearningOutcomeId);
        Task<StudentAssessmentScore> GetAssessmentScoreByAssessmentId(string? assessmentId);
        Task<IEnumerable<StudentLOScore>> GetLOScoreByAssessmentScoreId(string assessmentScoreId);
    }
}
