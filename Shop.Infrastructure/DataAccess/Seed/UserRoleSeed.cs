using Microsoft.AspNetCore.Identity;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.DataAccess.Seed
{
    public static class UserRoleSeed
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager, StoreContext context)
        {

            if (context.UserRoles.Any()) return;

            var adminUser = await userManager.FindByNameAsync("Admin");

            await userManager.AddToRoleAsync(adminUser, "Admin");

            var customerUser = await userManager.FindByNameAsync("Customer");

            await userManager.AddToRoleAsync(customerUser, "Customer");

        }
    }
}
