using LOTA.Model;

namespace LOTA.DataAccess.Repository.IRepository
{
    public interface IStudentCourseRepository : IRepository<StudentCourse, string>
    {
        Task<IEnumerable<StudentCourse>> GetByStudentIdAsync(string studentId);
        Task<IEnumerable<StudentCourse>> GetByCourseIdAsync(string courseId);
        Task<StudentCourse?> GetByStudentAndCourseAsync(string studentId, string courseId);
        /// <summary>
        /// Get student courses by course ID and trimester
        /// </summary>
        /// <param name="courseId">Course ID</param>
        /// <param name="academicYear">Academic year (optional)</param>
        /// <param name="trimesterNumber">Trimester number (optional)</param>
        /// <returns>List of student courses</returns>
        Task<IEnumerable<StudentCourse>> GetByCourseIdAndTrimesterAsync(string courseId, int? academicYear, int? trimesterNumber);
    }
}
