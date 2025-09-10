using LOTA.Model;

namespace LOTA.DataAccess.Repository.IRepository
{
    public interface ITrimesterCourseRepository
    {
        Task<IEnumerable<TrimesterCourse>> GetAllTrimesterCoursesAsync();
        Task<TrimesterCourse> GetTrimesterCourseByIdAsync(string id);
        Task<TrimesterCourse> CreateTrimesterCourseAsync(TrimesterCourse trimesterCourse);
        Task<TrimesterCourse> UpdateTrimesterCourseAsync(TrimesterCourse trimesterCourse);
        Task DeleteTrimesterCourseAsync(string id);
        Task<IEnumerable<TrimesterCourse>> GetTrimesterCoursesByTrimesterAsync(string trimesterId);
        Task<IEnumerable<TrimesterCourse>> GetTrimesterCoursesByCourseAsync(string courseId);
        Task<IEnumerable<TrimesterCourse>> GetTrimesterCoursesByTutorAsync(string tutorId);
        Task<IEnumerable<TrimesterCourse>> GetTrimesterCoursesByTrimesterAndCourseAsync(string trimesterId, string courseId);
        Task<bool> IsTrimesterCourseExistsAsync(string trimesterId, string courseId);
        Task<IEnumerable<TrimesterCourse>> GetLatestTrimesterCourseOfferingsAsync();
        Task<TrimesterCourse> GetTrimesterCourseWithDetailsAsync(string id);
        Task<IEnumerable<TrimesterCourse>> GetTrimesterCoursesByTutorAndTrimesterAsync(string tutorId, string trimesterId);
        
        /// <summary>
        /// Clear tutor assignment from all trimester courses by tutor ID
        /// </summary>
        /// <param name="tutorId">Tutor ID</param>
        void ClearTutorFromAllCourses(string tutorId);
    }
}
