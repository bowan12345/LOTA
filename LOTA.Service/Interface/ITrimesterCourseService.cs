using LOTA.Model.DTO.Admin;

namespace LOTA.Service.Interface
{
    public interface ITrimesterCourseService
    {
        Task<IEnumerable<TrimesterCourseReturnDTO>> GetAllTrimesterCoursesAsync();
        Task<TrimesterCourseReturnDTO> GetTrimesterCourseByIdAsync(string id);
        Task<TrimesterCourseReturnDTO> CreateTrimesterCourseAsync(TrimesterCourseCreateDTO trimesterCourse);
        Task<TrimesterCourseReturnDTO> UpdateTrimesterCourseAsync(TrimesterCourseUpdateDTO trimesterCourse);
        Task<bool> DeleteTrimesterCourseAsync(string id);
        Task<IEnumerable<TrimesterCourseReturnDTO>> GetTrimesterCoursesByTrimesterAsync(string trimesterId);
        Task<IEnumerable<TrimesterCourseReturnDTO>> GetTrimesterCoursesByCourseAsync(string courseId);
        Task<IEnumerable<TrimesterCourseReturnDTO>> GetTrimesterCoursesByTutorAsync(string tutorId);
        Task<bool> IsTrimesterCourseExistsAsync(string trimesterId, string courseId);
    }
}
