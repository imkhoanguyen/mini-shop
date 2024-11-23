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
                    FullName = "Admin",
                    UserName = "admin",
                    Email = "itk21sgu@gmail.com",
                    PhoneNumber = "0123456789",
                    Avatar = "user.jpg",
                },
                new AppUser
                {
                    FullName = "Admin2",
                    UserName = "admin2",
                    Email = "admin2@gmail.com",
                    PhoneNumber = "0123456789",
                    Avatar = "user.jpg",
                },
                new AppUser
                {
                    FullName = "Admin3",
                    UserName = "admin3",
                    Email = "admin3@gmail.com",
                    PhoneNumber = "0123456789",
                    Avatar = "user.jpg",
                },
                new AppUser
                {
                    FullName = "Customer",
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
