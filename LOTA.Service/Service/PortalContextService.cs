using LOTA.Model.DTO;
using LOTA.Service.Service.IService;
using LOTA.Utility;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace LOTA.Service.Service
{
    public class PortalContextService : IPortalContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        private readonly Dictionary<string, (string Area, string PortalName)> _roleConfig = new()
        {
            { Roles.Role_Admin, ("Admin", "Admin Portal") },
            { Roles.Role_Tutor, ("Tutor", "Tutor Portal") },
            { Roles.Role_Student, ("Student", "Student Portal") }
        };

        public PortalContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
        }

        public string GetCurrentRole()
        {
            if (!IsAuthenticated())
                return string.Empty;

            var user = _httpContextAccessor.HttpContext!.User;
            
            // Check roles in priority order (Admin > Tutor > Student)
            var rolePriority = new[] { Roles.Role_Admin, Roles.Role_Tutor, Roles.Role_Student };
            
            foreach (var role in rolePriority)
            {
                if (user.IsInRole(role))
                    return role;
            }

            return string.Empty;
        }

        public string GetCurrentArea()
        {
            var currentRole = GetCurrentRole();
            return _roleConfig.TryGetValue(currentRole, out var config) ? config.Area : string.Empty;
        }

        public string GetCurrentPortalName()
        {
            var currentRole = GetCurrentRole();
            return _roleConfig.TryGetValue(currentRole, out var config) ? config.PortalName : string.Empty;
        }
    }
}
