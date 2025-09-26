using LOTA.Model;
using System.Threading.Tasks;

namespace LOTA.Utility
{
    /// <summary>
    /// Extended email sender interface for LOTA system
    /// </summary>
    public interface ILOTAEmailSender
    {
        /// <summary>
        /// Send account creation email to user with login credentials
        /// </summary>
        /// <param name="user">User account</param>
        /// <param name="password">Generated password</param>
        /// <param name="userType">User type (Student/Tutor)</param>
        Task SendAccountCreationEmailAsync(ApplicationUser user, string password, string userType,string loginUrl);
    }
}
