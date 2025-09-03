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
                .ToListAsync();
        }

        public async Task<IEnumerable<StudentAssessmentScore>> GetStudentScoresByStudentAsync(string studentId)
        {
            return await _db.StudentAssessmentScore
                .Where(ss => ss.StudentId == studentId)
                .Include(ss => ss.Student)
                .Include(ss => ss.Assessment)
                .ToListAsync();
        }

        public async Task<IEnumerable<StudentAssessmentScore>> GetStudentScoresByCourseOfferingAsync(string courseOfferingId)
        {
            return await _db.StudentAssessmentScore
                .Where(ss => ss.Assessment.CourseOfferingId == courseOfferingId)
                .Include(ss => ss.Student)
                .Include(ss => ss.Assessment)
                .ToListAsync();
        }

        public void RemoveByCourseOfferingId(string courseOfferingId)
        {
            _db.StudentAssessmentScore
                .Where(ss => ss.Assessment.CourseOfferingId == courseOfferingId)
                .ExecuteDelete();
        }

        public void RemoveByAssessmentId(string assessmentId)
        {
            _db.StudentAssessmentScore
                .Where(ss => ss.AssessmentId == assessmentId)
                .ExecuteDelete();
        }

        public async Task<StudentAssessmentScore> GetStudentScoreByStudentAssessmentLOAsync(string studentId, string assessmentId, string loId)
        {
            return await _db.StudentAssessmentScore
                .FirstOrDefaultAsync(ss => ss.StudentId == studentId && 
                                          ss.AssessmentId == assessmentId);
        }

        public async Task<StudentAssessmentScore> GetStudentScoreByStudentAssessmentAsync(string studentId, string assessmentId)
        {
            return await _db.StudentAssessmentScore
                .FirstOrDefaultAsync(ss => ss.StudentId == studentId && 
                                          ss.AssessmentId == assessmentId);
        }

    }
}
