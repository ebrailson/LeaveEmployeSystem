using Microsoft.AspNetCore.Identity;

namespace LeaveEmployeSystem
{
    public static class SeedData
    {
        public static void Seed(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }
        private static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                var user = new IdentityUser { UserName = "admin", Email = "admin@gmail.com" };
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
