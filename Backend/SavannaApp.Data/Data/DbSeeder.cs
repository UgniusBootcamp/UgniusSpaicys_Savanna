using Microsoft.AspNetCore.Identity;
using SavannaApp.Data.Enums;

namespace SavannaApp.Data.Data
{
    public class DbSeeder(RoleManager<IdentityRole> roleManager)
    {
        public async Task SeedAsync() 
        {
            await AddDefaultRolesAsync();
        }

        private async Task AddDefaultRolesAsync()
        {
            foreach (var role in UserRoles.AllRoles)
            {
                var roleExists = await roleManager.RoleExistsAsync(role);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
