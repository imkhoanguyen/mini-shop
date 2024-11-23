using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.DataAccess.Seed
{
    public static class UserRoleSeed
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager, StoreContext context)
        {

            if (context.UserRoles.Any()) return;

            var adminUsers = await userManager.Users
               .Where(u => u.UserName.Contains("admin"))
               .ToListAsync();

            foreach (var adminUser in adminUsers)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            var customerUser = await userManager.FindByNameAsync("Customer");

            await userManager.AddToRoleAsync(customerUser, "Customer");

        }
    }
}
