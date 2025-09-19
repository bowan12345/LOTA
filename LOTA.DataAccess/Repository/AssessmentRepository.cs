using LOTA.DataAccess.Data;
using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;
using LOTA.Model.DTO.Admin;
using Microsoft.EntityFrameworkCore;

namespace LOTA.DataAccess.Repository
{
    public class AssessmentRepository : Repository<Assessment>, IAssessmentRepository
    {
        public AssessmentRepository(ApplicationDbContext db) : base(db){}

        public async Task AddLearningOutcomesAsync(List<AssessmentLearningOutcome> learningOutcomes)
        {
            await _db.AssessmentLearningOutcome.AddRangeAsync(learningOutcomes);
        }

        public async Task<IEnumerable<Assessment>> GetAssessmentsByCourseOfferingId(string? courseOfferingId)
        {
           return await _db.Assessment
               .Include(a => a.AssessmentType)
               .Include(a => a.AssessmentLearningOutcomes)
                   .ThenInclude(alo => alo.LearningOutcome)
               .Where(c => c.CourseOfferingId == courseOfferingId)
               .ToListAsync();
        }

        public async Task<IEnumerable<AssessmentLearningOutcome>> GetLOListByAssessmentId(string? assessmentId, string? includeProperties = null)
        {
            IQueryable<AssessmentLearningOutcome> query = _db.AssessmentLearningOutcome;
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

        public async Task<IEnumerable<AssessmentLearningOutcome>> GetAssessmentLOListByLOIds(IEnumerable<string> LOIds)
        {
            return await _db.AssessmentLearningOutcome
              .Where(c => LOIds.Contains(c.LOId))
              .ToListAsync();
        }

        public void RemoveLearningOutcomesByAssessmentIdAsync(string? assessmentId)
        {
            _db.AssessmentLearningOutcome.Where(e => e.AssessmentId == assessmentId).ExecuteDelete();
        }

        public void RemoveAssessmentLearningOutcomeById(string? assessmentLearningOutcomeId)
        {
            _db.AssessmentLearningOutcome.Where(e => e.Id == assessmentLearningOutcomeId).ExecuteDelete();
        }

        public void UpdateAssessmentLearningOutcome(AssessmentLearningOutcome learningOutcome)
        {
            _db.AssessmentLearningOutcome.Update(learningOutcome);
        }

        // Use JOIN query to retrieve assessments with learning outcomes
        public async Task<IEnumerable<AssessmentWithLOsDTO>> GetAssessmentsWithLOsByCourseOfferingId(string courseOfferingId)
        {
            return await _db.Assessment
                .Where(a => a.CourseOfferingId == courseOfferingId)
                .Select(a => new AssessmentWithLOsDTO
                {
                    AssessmentId = a.Id,
                    AssessmentName = a.AssessmentName,
                    MaxAssessmentScore = a.Score,
                    Weight = a.Weight,
                    AssessmentLearningOutcomes = a.AssessmentLearningOutcomes.Select(alo => new AssessmentLOWithScoreDTO
                    {
                        Id = alo.Id,
                        LOId = alo.LOId,
                        Score = alo.Score,
                        LearningOutcome = alo.LearningOutcome
                    }).ToList()
                })
                .ToListAsync();
        }

        public void RemoveAssessmentsByCourseOfferingId(string courseOfferingId)
        {
            _db.Assessment.Where(e => e.CourseOfferingId == courseOfferingId).ExecuteDelete();
        }
    }
}
