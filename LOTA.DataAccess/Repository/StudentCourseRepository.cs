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
                .Include(sc => sc.Course)
                .Where(sc => sc.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<StudentCourse>> GetByCourseIdAsync(string courseId)
        {
            return await _db.StudentCourse
                .Include(sc => sc.Student)
                .Where(sc => sc.CourseId == courseId)
                .ToListAsync();
        }

        public async Task<StudentCourse?> GetByStudentAndCourseAsync(string studentId, string courseId)
        {
            return await _db.StudentCourse
                .Include(sc => sc.Course)
                .Include(sc => sc.Student)
                .FirstOrDefaultAsync(sc => sc.StudentId == studentId && sc.CourseId == courseId);
        }
    }
}
