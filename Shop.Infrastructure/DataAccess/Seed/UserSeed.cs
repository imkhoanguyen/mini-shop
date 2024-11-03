using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.DataAccess.Seed
{
    public static class UserSeed
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var users = new List<AppUser>()
            {
                new AppUser
                {
                    Fullname = "Admin",
                    UserName = "admin",
                    Email = "itk21sgu@gmail.com",
                    PhoneNumber = "0123456789",
                    Avatar = "user.jpg",
                },
                new AppUser
                {
                    Fullname = "Customer",
                    UserName = "customer",
                    Email = "khoasgu01@gmail.com",
                    PhoneNumber = "0987654321",
                    Avatar = "user.jpg",
                },
            };

            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "Admin_123");
            }
        }
    }
}
