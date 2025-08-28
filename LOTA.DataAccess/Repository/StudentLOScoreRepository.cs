using LOTA.DataAccess.Data;
using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;
using Microsoft.EntityFrameworkCore;

namespace LOTA.DataAccess.Repository
{
    public class StudentLOScoreRepository : Repository<StudentLOScore>, IStudentLOScoreRepository
    {
        private readonly ApplicationDbContext _db;

        public StudentLOScoreRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<StudentLOScore>> GetStudentLOScoresByStudentAssessmentScoreAsync(string studentAssessmentScoreId)
        {
            return await _db.StudentLOScore
                .Where(s => s.StudentAssessmentScoreId == studentAssessmentScoreId && s.IsActive)
                .ToListAsync();
        }

        public async Task<StudentLOScore> GetStudentLOScoreByStudentAssessmentScoreAndLOAsync(string studentAssessmentScoreId, string loId)
        {

            return await _db.StudentLOScore
                .FirstOrDefaultAsync(s => s.StudentAssessmentScoreId == studentAssessmentScoreId && s.IsActive);
        }

        public async Task<StudentLOScore> GetStudentLOScoreByStudentAssessmentAndLOAsync(string studentAssessmentScoreId, string assessmentLearningOutcomeId)
        {
            return await _db.StudentLOScore
                .FirstOrDefaultAsync(s => s.StudentAssessmentScoreId == studentAssessmentScoreId && 
                                         s.AssessmentLearningOutcomeId == assessmentLearningOutcomeId && 
                                         s.IsActive);
        }

        public async Task<StudentAssessmentScore> GetAssessmentScoreByAssessmentId(string? assessmentId)
        {
            return await _db.StudentAssessmentScore.FirstOrDefaultAsync(s => s.AssessmentId == assessmentId);
        }

        public async Task<IEnumerable<StudentLOScore>> GetLOScoreByAssessmentScoreId(string assessmentScoreId)
        {
            return await _db.StudentLOScore.Where(s => s.StudentAssessmentScoreId == assessmentScoreId).ToListAsync();
        }
    }
}
