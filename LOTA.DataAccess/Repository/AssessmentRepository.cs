using LOTA.DataAccess.Data;
using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;
using Microsoft.EntityFrameworkCore;

namespace LOTA.DataAccess.Repository
{
    public class AssessmentRepository : Repository<Assessment>, IAssessmentRepository
    {
        private readonly DbSet<AssessmentLearningOutcome> _assessmentLearningOutcomesDb;
        public AssessmentRepository(ApplicationDbContext db) : base(db)
        {
            _assessmentLearningOutcomesDb = db.Set<AssessmentLearningOutcome>();
        }

        public async Task AddLearningOutcomesAsync(List<AssessmentLearningOutcome> learningOutcomes)
        {
            await _db.AssessmentLearningOutcome.AddRangeAsync(learningOutcomes);
        }

        public async Task<IEnumerable<Assessment>> GetAssessmentsByCourseOfferingId(string? courseOfferingId)
        {
           return await _db.Assessment.Where(c => c.CourseOfferingId == courseOfferingId).ToListAsync();
        }

        public async Task<IEnumerable<AssessmentLearningOutcome>> GetLOListByAssessmentId(string? assessmentId, string? includeProperties = null)
        {
            IQueryable<AssessmentLearningOutcome> query = _assessmentLearningOutcomesDb;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            query = query.Where(c => c.AssessmentId == assessmentId);
            return await query.ToListAsync();
        }

        public void RemoveLearningOutcomesByAssessmentIdAsync(string? assessmentId)
        {
            _assessmentLearningOutcomesDb.Where(e => e.AssessmentId == assessmentId).ExecuteDelete();
        }
    }
}
