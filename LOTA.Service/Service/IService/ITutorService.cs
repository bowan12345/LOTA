using LOTA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.Service.Service.IService
{
    public interface ITutorService
    {
        /// <summary>
        /// get tutor info by email
        /// </summary>
        /// <param name="email"> email of tutor </param>
        /// <returns> an object of tutor</returns>
        Task<ApplicationUser> GetTutorByEmailAsync(string email);

        /// <summary>
        /// get all tutors
        /// </summary>
        /// <returns> a list of tutors</returns>
        Task<IEnumerable<ApplicationUser>> GetAllTutorsAsync();

        /// <summary>
        /// add assigned courses to a tutor
        /// </summary>
        /// <param name="TutorId"> id of tutor </param>
        /// <param name="AssignedCourses"> a list of assigned courses </param>
        /// <returns></returns>
        Task AddTutorCourseAsync(string TutorId, List<string> AssignedCourses);

        /// <summary>
        /// get tutor info by id
        /// </summary>
        /// <param name="id"> id of tutor </param>
        /// <returns> an object of tutor</returns>
        Task<ApplicationUser> GetTutorByIdAsync(string id);

        /// <summary>
        /// get assigned courses for a tutor
        /// </summary>
        /// <param name="tutorId"> id of tutor </param>
        /// <returns> a list of tutor courses</returns>
        Task<IEnumerable<TutorCourse>> GetTutorCoursesAsync(string tutorId);

        /// <summary>
        /// remove all course assignments for a tutor
        /// </summary>
        /// <param name="tutorId"> id of tutor </param>
        /// <returns></returns>
        Task RemoveAllTutorCoursesAsync(string tutorId);
    }
}
