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
        /// get tutor info by id
        /// </summary>
        /// <param name="id"> id of tutor </param>
        /// <returns> an object of tutor</returns>
        Task<ApplicationUser> GetTutorByIdAsync(string id);


        /// <summary>
        /// search tutors by search term
        /// </summary>
        /// <param name="searchTerm"> search term to filter tutors </param>
        /// <returns> filtered list of tutors</returns>
        Task<IEnumerable<ApplicationUser>> SearchTutorsAsync(string searchTerm);

        /// <summary>
        /// Delete a tutor and handle all related data
        /// </summary>
        /// <param name="id"> tutor id </param>
        Task DeleteTutorAsync(string id);

        /// <summary>
        /// Delete multiple tutors and handle all related data
        /// </summary>
        /// <param name="ids"> list of tutor ids </param>
        Task DeleteTutorsAsync(IEnumerable<string> ids);
    }
}
