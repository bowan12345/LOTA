using LOTA.Model;
using LOTA.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

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
            var logger = scope.ServiceProvider.GetService<ILoggerFactory>()?.CreateLogger("IdentitySeeder");

            string[] roles = { Roles.Role_Admin, Roles.Role_Tutor, Roles.Role_Student };
            foreach (var role in roles)
            {
                if (!roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
                {
                    roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
                }
            }

            string adminEmail = FirstNonEmpty(
                configuration?["Admin_Email"],
                configuration?["ADMIN_EMAIL"]) ?? "admin@weltec.ac.nz";
            string adminPassword = FirstNonEmpty(
                configuration?["Admin_Password"],
                configuration?["ADMIN_PASSWORD"]) ?? "Admin123!";

            bool forceReset = bool.TryParse(
                FirstNonEmpty(configuration?["Admin_ForceReset"], configuration?["ADMIN_FORCE_RESET"]),
                out bool resetValue) && resetValue;

            if (forceReset)
            {
                var oldAdmins = userManager.GetUsersInRoleAsync(Roles.Role_Admin).GetAwaiter().GetResult();
                foreach (var oldAdmin in oldAdmins)
                {
                    userManager.DeleteAsync(oldAdmin).GetAwaiter().GetResult();
                }
            }

            var adminUser = userManager.FindByEmailAsync(adminEmail).GetAwaiter().GetResult();
            if (adminUser != null)
            {
                logger?.LogInformation("Admin account already exists: {Email}", adminEmail);
                return;
            }

            var newAdmin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                FirstName = FirstNonEmpty(configuration?["Admin_FirstName"], configuration?["ADMIN_FIRST_NAME"]) ?? "Admin",
                LastName = FirstNonEmpty(configuration?["Admin_LastName"], configuration?["ADMIN_LAST_NAME"]) ?? "Weltec",
                IsActive = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = userManager.CreateAsync(newAdmin, adminPassword).GetAwaiter().GetResult();
            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(newAdmin, Roles.Role_Admin).GetAwaiter().GetResult();
                logger?.LogInformation("Admin account created: {Email}", adminEmail);
                Console.WriteLine($"Admin account created: {adminEmail}");
            }
            else
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                logger?.LogError("Failed to create admin {Email}: {Errors}", adminEmail, errors);
                Console.WriteLine($"Failed to create admin {adminEmail}: {errors}");
            }
        }

        private static string? FirstNonEmpty(params string?[] values)
        {
            return values.FirstOrDefault(v => !string.IsNullOrWhiteSpace(v));
        }
    }
}
