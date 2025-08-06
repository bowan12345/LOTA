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
        /// add a new tutor
        /// </summary>
        /// <param name="user"> an object of tutor </param>
        /// <returns></returns>
        Task AddTutorAsync(ApplicationUser user);
    }
}
