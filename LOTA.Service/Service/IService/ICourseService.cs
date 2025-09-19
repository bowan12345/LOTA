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

        Task<IEnumerable<CourseReturnDTO>> GetCourseByIdsAsync(List<string> courseIds);

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
        /// remove a list courses
        /// </summary>
        /// <param name="courseId"> The course ids to remove </param>
        /// <returns></returns>
        Task RemoveRangeCourse(List<string> courseIds);

        /// <summary>
        /// Import courses from Excel file
        /// </summary>
        /// <param name="fileStream">Excel file stream</param>
        /// <param name="qualificationId">Qualification ID to assign to all courses</param>
        /// <returns>Import result with success count and errors</returns>
        Task<(int successCount, List<string> errors)> ImportCoursesFromExcelAsync(Stream fileStream, string qualificationId);

        /// <summary>
        /// Generate Excel template for course import
        /// </summary>
        /// <returns>Excel file as byte array</returns>
        Task<byte[]> GenerateExcelTemplateAsync();

        /// <summary>
        /// Add students to a course
        /// </summary>
        /// <param name="courseOfferingId">The offered course ID</param>
        /// <param name="studentIds">List of student IDs to add</param>
        /// <param name="trimesterId">Trimester ID</param>
        /// <returns></returns>
        Task AddStudentsToCourseOfferingAsync(string courseOfferingId, List<string> studentIds, string trimesterId);

        /// <summary>
        /// Import students to a offered course from Excel file
        /// </summary>
        /// <param name="CourseOfferingId">The offered course ID</param>
        /// <param name="trimesterId">Trimester ID</param>
        /// <param name="fileStream">Excel file stream</param>
        /// <returns>Import result with success count and errors</returns>
        Task<(int successCount, List<string> errors)> ImportStudentsFromExcelCourseOfferingAsync(string courseOfferingId, string trimesterId, Stream fileStream);

        /// <summary>
        /// Remove a student from a courseoffering
        /// </summary>
        /// <param name="courseOfferingId">The offered course ID</param>
        /// <param name="studentId">The student ID to remove</param>
        /// <returns></returns>
        Task RemoveStudentFromCourseOfferingAsync(string courseOfferingId, string studentId);

        /// <summary>
        /// Generate Excel template for uploading students to course
        /// </summary>
        /// <returns>Excel file as byte array</returns>
        Task<byte[]> GenerateStudentsExcelTemplateAsync();
    }
}
