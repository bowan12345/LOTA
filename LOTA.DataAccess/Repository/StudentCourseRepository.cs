using LOTA.DataAccess.Repository.IRepository;
using LOTA.Model;
using LOTA.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace LOTA.DataAccess.Repository
{
    public class StudentCourseRepository : Repository<StudentCourse>, IStudentCourseRepository
    {
        public StudentCourseRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<IEnumerable<StudentCourse>> GetByStudentIdAsync(string studentId)
        {
            return await _db.StudentCourse
                .Include(sc => sc.TrimesterCourse)
                .Where(sc => sc.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<StudentCourse>> GetByCourseOfferingIdAsync(string courseOfferingId)
        {
            return await _db.StudentCourse
                .Include(sc => sc.Student)
                .Include(sc => sc.TrimesterCourse)
                .Where(sc => sc.TrimesterCourse.Id == courseOfferingId)
                .ToListAsync();
        }

        public async Task<IEnumerable<StudentCourse>> GetByCourseOfferingIdAndTrimesterAsync(string courseOfferingId,string trimesterId)
        {
            var query = _db.StudentCourse
                .Include(sc => sc.Student)
                .Include(sc => sc.TrimesterCourse)
                .Where(sc => sc.TrimesterCourse.Id == courseOfferingId && sc.TrimesterId == trimesterId);

            return await query.ToListAsync();
        }

        public async Task<StudentCourse?> GetByStudentAndCourseAsync(string studentId, string courseOfferingId)
        {
            return await _db.StudentCourse
                .Include(sc => sc.TrimesterCourse)
                .Include(sc => sc.Student)
                .FirstOrDefaultAsync(sc => sc.StudentId == studentId && sc.TrimesterCourse.Id == courseOfferingId);
        }

        public void RemoveAllByTrimesterId(string id)
        {
            dbset.Where(e => EF.Property<string>(e, "TrimesterId") == id).ExecuteDelete();
        }

        public void RemoveAllByTrimesterIds(IEnumerable<string> ids)
        {
            dbset.Where(e => ids.Contains(EF.Property<string>(e, "TrimesterId"))).ExecuteDelete();
        }

        public void RemoveEnrolledStudentByCourseOfferingId(string courseOfferingId)
        {
            dbset.Where(e =>e.CourseOfferingId == courseOfferingId).ExecuteDelete();
        }

        public void RemoveAllByStudentId(string studentId)
        {
            dbset.Where(e => e.StudentId == studentId).ExecuteDelete();
        }
    }
}
