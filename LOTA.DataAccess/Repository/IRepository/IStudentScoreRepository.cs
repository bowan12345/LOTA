using LOTA.Model;

namespace LOTA.DataAccess.Repository.IRepository
{
    public interface IStudentScoreRepository : IRepository<StudentScore,string>
    {
        Task<IEnumerable<StudentScore>> GetStudentScoresByAssessmentAsync(string assessmentId);
        Task<IEnumerable<StudentScore>> GetStudentScoresByStudentAsync(string studentId);
        Task<IEnumerable<StudentScore>> GetStudentScoresByCourseOfferingAsync(string courseOfferingId);
        Task<StudentScore> GetStudentScoreByStudentAssessmentLOAsync(string studentId, string assessmentId, string loId);
    }
}
