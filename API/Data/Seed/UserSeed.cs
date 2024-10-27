using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace API.Data.Seed
{
    public static class UserSeed
    {
        public static    async Task SeedAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
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
                    Fullname = "Admin",
                    UserName = "admin1",
                    Email = "itk22sgu@gmail.com",
                    PhoneNumber = "0123456789",
                    Avatar = "user.jpg",
                },
                new AppUser
                {
                    Fullname = "Admin",
                    UserName = "admin2",
                    Email = "itk23sgu@gmail.com",
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
                var result = await userManager.CreateAsync(user, "Admin_123");
                if (result.Succeeded)
                {
                    // Gán vai trò "Admin" cho các tài khoản admin
                    if (user.UserName.StartsWith("admin"))
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                    else if (user.UserName == "customer")
                    {
                        await userManager.AddToRoleAsync(user, "Customer");
                    }
                }
            }
        }
    }
}
