using LOTA.Model.DTO.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Service.Service.IService
{
    public interface ITrimesterCourseService
    {
        Task<IEnumerable<TrimesterCourseReturnDTO>> GetAllTrimesterCoursesAsync();
        Task<TrimesterCourseReturnDTO> GetTrimesterCourseByIdAsync(string id);
        Task<TrimesterCourseReturnDTO> CreateTrimesterCourseAsync(TrimesterCourseCreateDTO trimesterCourse);
        Task<TrimesterCourseReturnDTO> UpdateTrimesterCourseAsync(TrimesterCourseUpdateDTO trimesterCourse);
        Task DeleteTrimesterCourseAsync(string id);
        Task<IEnumerable<TrimesterCourseReturnDTO>> GetTrimesterCoursesByTrimesterAsync(string trimesterId);
        Task<IEnumerable<TrimesterCourseReturnDTO>> GetTrimesterCoursesByCourseAsync(string courseId);
        Task<IEnumerable<TrimesterCourseReturnDTO>> GetTrimesterCoursesByTutorAsync(string tutorId);
        Task<IEnumerable<TrimesterCourseReturnDTO>> GetTrimesterCoursesByTutorAndTrimesterAsync(string tutorId, string trimesterId);
        Task<bool> IsTrimesterCourseExistsAsync(string trimesterId, string courseId);
        Task<IEnumerable<CourseOfferingReturnDTO>> GetLatestTrimesterCourseOfferingsAsync();
    }
}
