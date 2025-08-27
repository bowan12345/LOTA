using LOTA.DataAccess.Repository.IRepository;
using LOTA.DataAccess.Data;
using LOTA.Model;
using Microsoft.EntityFrameworkCore;

namespace LOTA.DataAccess.Repository
{
    public class StudentScoreRepository : Repository<StudentAssessmentScore>, IStudentScoreRepository
    {
        public StudentScoreRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<IEnumerable<StudentAssessmentScore>> GetStudentScoresByAssessmentAsync(string assessmentId)
        {
            return await _db.StudentAssessmentScore
                .Where(ss => ss.AssessmentId == assessmentId)
                .Include(ss => ss.Student)
                .Include(ss => ss.Assessment)
                .Include(ss => ss.LearningOutcome)
                .Include(ss => ss.Trimester)
                .ToListAsync();
        }

        public async Task<IEnumerable<StudentAssessmentScore>> GetStudentScoresByStudentAsync(string studentId)
        {
            return await _db.StudentAssessmentScore
                .Where(ss => ss.StudentId == studentId)
                .Include(ss => ss.Student)
                .Include(ss => ss.Assessment)
                .Include(ss => ss.LearningOutcome)
                .Include(ss => ss.Trimester)
                .ToListAsync();
        }

        public async Task<IEnumerable<StudentAssessmentScore>> GetStudentScoresByCourseOfferingAsync(string courseOfferingId)
        {
            return await _db.StudentAssessmentScore
                .Where(ss => ss.Assessment.CourseOfferingId == courseOfferingId)
                .Include(ss => ss.Student)
                .Include(ss => ss.Assessment)
                .Include(ss => ss.LearningOutcome)
                .Include(ss => ss.Trimester)
                .ToListAsync();
        }

        public async Task<StudentAssessmentScore> GetStudentScoreByStudentAssessmentLOAsync(string studentId, string assessmentId, string loId)
        {
            return await _db.StudentAssessmentScore
                .FirstOrDefaultAsync(ss => ss.StudentId == studentId && 
                                          ss.AssessmentId == assessmentId && 
                                          ss.LOId == loId);
        }
    }
}
