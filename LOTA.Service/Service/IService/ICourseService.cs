using LOTA.Model;
using LOTA.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Service.Service.IService
{
    public interface ICourseService
    {
        /// <summary>
        /// Create a new course
        /// </summary>
        /// <param name="course"> The course to create </param>
        /// <returns> The created course </returns>
         Task<Course> CreateCourseAsync(Course course);

        /// <summary>
        /// Get all courses
        /// </summary>
        /// <returns> All courses </returns>
         Task<IEnumerable<CourseReturnDTO>> GetAllCoursesAsync();

        /// <summary>
        ///  Get a course by course name or course code
        /// </summary>
        /// <param name="courseSearchItem"> course name or course code</param>
        /// <returns> a list courses </returns>
         Task<IEnumerable<CourseReturnDTO>> GetCoursesByNameOrCodeAsync(string courseSearchItem);

        /// <summary>
        ///  update details of the course
        /// </summary>
        /// <param name="course"> The course to update </param>
        /// <returns></returns>
        Task UpdateCourse(Course course);

        /// <summary>
        /// remove a course
        /// </summary>
        /// <param name="courseId"> The course id to remove </param>
        /// <returns></returns>
        Task RemoveCourse(string courseId);
    }
}
