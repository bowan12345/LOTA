using LOTA.Model;

namespace LOTA.DataAccess.Repository.IRepository
{
    public interface IStudentScoreRepository : IRepository<StudentAssessmentScore,string>
    {
        Task<IEnumerable<StudentAssessmentScore>> GetStudentScoresByAssessmentAsync(string assessmentId);
        Task<IEnumerable<StudentAssessmentScore>> GetStudentScoresByStudentAsync(string studentId);
        Task<IEnumerable<StudentAssessmentScore>> GetStudentScoresByCourseOfferingAsync(string courseOfferingId);
        Task<StudentAssessmentScore> GetStudentScoreByStudentAssessmentLOAsync(string studentId, string assessmentId, string loId);
        Task<StudentAssessmentScore> GetStudentScoreByStudentAssessmentAsync(string studentId, string assessmentId);
        void RemoveByCourseOfferingId(string courseOfferingId);
        void RemoveByAssessmentId(string assessmentId);
        
        /// <summary>
        /// Delete all student assessment scores by student ID
        /// </summary>
        /// <param name="studentId">Student ID</param>
        void RemoveAllByStudentId(string studentId);

    }
}
