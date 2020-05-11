using LeaveEmployeSystem.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace LeaveEmployeSystem
{
    public static class SeedData
    {
        public static void Seed(UserManager<Employee> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }
        private static void SeedUsers(UserManager<Employee> userManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                var user = new Employee { UserName = "admin@gmail.com", Email = "admin@gmail.com" };
                var result = userManager.CreateAsync(user, "Loveprogram98@").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admnistrator").Wait();
                }
            }
        }
        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admnistrator").Result)
            {
                var role = new IdentityRole { Name = "Admnistrator" };
                var result = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Employee").Result)
            {
                var role = new IdentityRole { Name = "Employee" };
                var result = roleManager.CreateAsync(role).Result;
            }


        }


    }
}
