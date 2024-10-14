using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Identity;

namespace api.Data.Seed
{
    public class RoleSeed
    {
        public static async Task SeedAsync(RoleManager<AppRole> roleManager)
        {
            if (roleManager.Roles.Any()) return;

            var roles = new List<AppRole>()
                {
                    new AppRole{Name = "Admin", Description = "Management system"},
                    new AppRole{Name = "Customer", Description = "Shopping"},
                };


            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }
        }
    }
}
