using LOTA.Model;
using LOTA.Model.DTO;
using LOTA.Model.DTO.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LOTA.Service.Service.IService
{
    public interface ICourseService
    {
        /// <summary>
        /// Create a new course
        /// </summary>
        /// <param name="course"> The course to create </param>
        /// <returns> The created course </returns>
         Task<CourseReturnDTO> CreateCourseAsync(CourseCreateDTO course);

        /// <summary>
        /// Get a course by course code
        /// </summary>
        /// <param name="courseCode"> The course code to search </param>
        /// <returns> one course </returns>
        Task<CourseReturnDTO> GetCourseByCodeAsync(string courseCode);

        /// <summary>
        /// Get a course by course ID
        /// </summary>
        /// <param name="courseId"> The course ID to search </param>
        /// <returns> one course </returns>
        Task<CourseReturnDTO> GetCourseByIdAsync(string courseId);

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
        Task UpdateCourse(CourseUpdateDTO course);

        /// <summary>
        /// remove a course
        /// </summary>
        /// <param name="courseId"> The course id to remove </param>
        /// <returns></returns>
        Task RemoveCourse(string courseId);

        /// <summary>
        /// Import courses from Excel file
        /// </summary>
        /// <param name="fileStream">Excel file stream</param>
        /// <returns>Import result with success count and errors</returns>
        Task<(int successCount, List<string> errors)> ImportCoursesFromExcelAsync(Stream fileStream);

        /// <summary>
        /// Generate Excel template for course import
        /// </summary>
        /// <returns>Excel file as byte array</returns>
        Task<byte[]> GenerateExcelTemplateAsync();
    }
}
