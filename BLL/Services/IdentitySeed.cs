using HealthCare.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace HealthCare.Services
{
    public static class IdentitySeed
    {
        public static void CreateRolesAndAdminUser(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            const string adminRoleName = "Admin";

            string[] roleNames = { adminRoleName, "Patient" };

            foreach (string roleName in roleNames)
            {
                CreateRole(serviceProvider, roleName);
            }

            string firstName = configuration["AdminUser:FirstName"]!;
            string lastName = configuration["AdminUser:LastName"]!;
            string email = configuration["AdminUser:Email"]!;
            string password = configuration["AdminUser:Password"]!;
            AddUserToRole(serviceProvider, email, password, firstName, lastName, adminRoleName);
        }

        private static void CreateRole(IServiceProvider serviceProvider, string roleName)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            Task<bool> roleExists = roleManager.RoleExistsAsync(roleName);
            roleExists.Wait();

            if (!roleExists.Result)
            {
                Task<IdentityResult> roleResult = roleManager.CreateAsync(new IdentityRole(roleName));
                roleResult.Wait();
            }
        }

        private static void AddUserToRole(IServiceProvider serviceProvider, string userEmail,
        string userPwd, string firstName, string lastName, string roleName)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            Task<ApplicationUser> checkAppUser = userManager!.FindByEmailAsync(userEmail)!;
            checkAppUser.Wait();

            ApplicationUser appUser = checkAppUser.Result;

            if (checkAppUser.Result == null)
            {
                ApplicationUser newAppUser = new ApplicationUser
                {
                    Email = userEmail,
                    UserName = userEmail,
                    FirstName = firstName,
                    LastName = lastName
                };

                Task<IdentityResult> taskCreateAppUser = userManager.CreateAsync(newAppUser, userPwd);
                taskCreateAppUser.Wait();

                if (taskCreateAppUser.Result.Succeeded)
                {
                    appUser = newAppUser;
                }
            }

            Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(appUser, roleName);
            newUserRole.Wait();
        }
    }
}
