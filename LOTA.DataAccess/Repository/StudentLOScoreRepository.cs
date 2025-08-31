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

        public async Task<IEnumerable<StudentLOScore>> GetByStudentAndLearningOutcomeAsync(string studentId, string learningOutcomeName)
        {
            return await _db.StudentLOScore
                .Include(s => s.StudentAssessmentScore)
                .Include(s => s.StudentAssessmentScore.Assessment)
                .Include(s => s.StudentAssessmentScore.Assessment.AssessmentLearningOutcomes)
                .Include(s => s.StudentAssessmentScore.Assessment.AssessmentLearningOutcomes)
                .ThenInclude(alo => alo.LearningOutcome)
                .Where(s => s.StudentAssessmentScore.StudentId == studentId && 
                           s.StudentAssessmentScore.Assessment.AssessmentLearningOutcomes
                               .Any(alo => alo.LearningOutcome.LOName == learningOutcomeName))
                .ToListAsync();
        }

        // Batch retrieve all LO scores for a course offering
        public async Task<IEnumerable<StudentLOScore>> GetLOScoresByCourseOfferingAsync(string courseOfferingId)
        {
            return await _db.StudentLOScore
                .Include(s => s.StudentAssessmentScore)
                .Include(s => s.StudentAssessmentScore.Student)
                .Include(s => s.AssessmentLearningOutcome)
                .Include(s => s.AssessmentLearningOutcome.LearningOutcome)
                .Where(s => s.StudentAssessmentScore.Assessment.CourseOfferingId == courseOfferingId)
                .OrderByDescending(s => s.CreatedDate)  // Order by creation date to ensure latest scores come first
                .ToListAsync();
        }
    }
}
