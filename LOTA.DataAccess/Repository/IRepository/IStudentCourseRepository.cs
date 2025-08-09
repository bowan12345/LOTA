using LOTA.Model;

namespace LOTA.DataAccess.Repository.IRepository
{
    public interface IStudentCourseRepository : IRepository<StudentCourse, string>
    {
        Task<IEnumerable<StudentCourse>> GetByStudentIdAsync(string studentId);
        Task<IEnumerable<StudentCourse>> GetByCourseIdAsync(string courseId);
        Task<StudentCourse?> GetByStudentAndCourseAsync(string studentId, string courseId);
    }
}
