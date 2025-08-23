using LOTA.Model;

namespace LOTA.DataAccess.Interface
{
    public interface ITrimesterCourseRepository
    {
        Task<IEnumerable<TrimesterCourse>> GetAllTrimesterCoursesAsync();
        Task<TrimesterCourse> GetTrimesterCourseByIdAsync(string id);
        Task<TrimesterCourse> CreateTrimesterCourseAsync(TrimesterCourse trimesterCourse);
        Task<TrimesterCourse> UpdateTrimesterCourseAsync(TrimesterCourse trimesterCourse);
        Task<bool> DeleteTrimesterCourseAsync(string id);
        Task<IEnumerable<TrimesterCourse>> GetTrimesterCoursesByTrimesterAsync(string trimesterId);
        Task<IEnumerable<TrimesterCourse>> GetTrimesterCoursesByCourseAsync(string courseId);
        Task<IEnumerable<TrimesterCourse>> GetTrimesterCoursesByTutorAsync(string tutorId);
        Task<IEnumerable<TrimesterCourse>> GetTrimesterCoursesByTrimesterAndCourseAsync(string trimesterId, string courseId);
        Task<bool> IsTrimesterCourseExistsAsync(string trimesterId, string courseId);
    }
}
