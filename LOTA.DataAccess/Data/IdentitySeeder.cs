using LOTA.Model;
using LOTA.Utility;
using Microsoft.AspNetCore.Identity;
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

            string[] roles = { Roles.Role_Admin, Roles.Role_Tutor, Roles.Role_Student };
            foreach (var role in roles)
            {
                if (!roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
                {
                    roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
                }
            }

            // 2. initial admin account
            string adminEmail = "admin@weltec.ac.nz";
            string adminPassword = "Admin123!"; 
            var adminUser = userManager.FindByEmailAsync(adminEmail).GetAwaiter().GetResult();

            if (adminUser == null)
            {
                var newAdmin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FirstName = "Admin",
                    LastName = "Weltec",
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
