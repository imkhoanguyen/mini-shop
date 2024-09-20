using System.Security.Claims;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> FindUserByClaimsPrincipleWithAddress(this UserManager<AppUser> userManager, 
            ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);

            return await userManager.Users.Include(x => x.Address)
                .SingleOrDefaultAsync(x => x.Email == email) ?? throw new InvalidOperationException("User not found");
        }

        public static async Task<AppUser> FindByEmailFromClaimsPrincipal(this UserManager<AppUser> userManager, 
            ClaimsPrincipal user)
        {
            return await userManager.Users
                .SingleOrDefaultAsync(x => x.Email == user.FindFirstValue(ClaimTypes.Email)) ?? throw new InvalidOperationException("User not found");
        }
        public static string GetUsername(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value ?? throw new InvalidOperationException("User not found");
        }
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var userIdValue = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new InvalidOperationException("User not found");
            return int.Parse(userIdValue);
        }
       
    }
}