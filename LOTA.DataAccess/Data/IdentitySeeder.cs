using LOTA.Model;
using LOTA.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTA.DataAccess.Data
{
    public static class IdentitySeeder
    {

        public static void SetRolesAndAdmin(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateAsyncScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var configuration = scope.ServiceProvider.GetService<IConfiguration>();

            string[] roles = { Roles.Role_Admin, Roles.Role_Tutor, Roles.Role_Student };
            foreach (var role in roles)
            {
                if (!roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
                {
                    roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
                }
            }

            // 2. initial admin account read from configuration on Azure app setting
            string adminEmail = configuration?["Admin_Email"] ?? "admin@weltec.ac.nz";
            string adminPassword = configuration?["Admin_Password"] ?? "Admin123!"; 
            //read force reset switch on Azure app setting
            bool forceReset = bool.TryParse(configuration?["Admin_ForceReset"], out bool resetValue) && resetValue;
            // if force reset is true, then delete all previous admin account data 
            if (forceReset)
            {
                var oldAdmins = userManager.GetUsersInRoleAsync(Roles.Role_Admin).GetAwaiter().GetResult();
                foreach (var oldAdmin in oldAdmins)
                {
                    userManager.DeleteAsync(oldAdmin).GetAwaiter().GetResult();
                }
            }

            var adminUser = userManager.FindByEmailAsync(adminEmail).GetAwaiter().GetResult();
            if (adminUser == null)
            {
                var newAdmin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FirstName = configuration?["Admin_FirstName"] ?? "Admin",
                    LastName = configuration?["Admin_LastName"] ?? "Weltec",
                    IsActive = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = userManager.CreateAsync(newAdmin, adminPassword).GetAwaiter().GetResult();
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(newAdmin, Roles.Role_Admin).GetAwaiter().GetResult();
                }
            }

        }
    }
}
