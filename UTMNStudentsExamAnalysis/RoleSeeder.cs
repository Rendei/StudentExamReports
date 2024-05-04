using Microsoft.AspNetCore.Identity;

namespace UTMNStudentsExamAnalysis
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                var adminRole = new IdentityRole("Admin");
                await roleManager.CreateAsync(adminRole);
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                var userRole = new IdentityRole("User");
                await roleManager.CreateAsync(userRole);
            }
        }
    }
}
