using LOTA.Model;
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
         Task<IEnumerable<Course>> GetAllCoursesAsync();
    }
}
