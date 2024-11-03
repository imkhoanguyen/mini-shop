using System.Security.Claims;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;

namespace API.Extensions
{
    public static class UserManagerExtensions
    {
        // Finds a user by ClaimsPrincipal and includes their address in the query
        public static async Task<AppUser> FindUserByClaimsPrincipleWithAddress(this UserManager<AppUser> userManager, 
            ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);

            if (email == null)
                throw new InvalidOperationException("Email claim not found.");

            return await userManager.Users.Include(x => x.Address)
                .SingleOrDefaultAsync(x => x.Email == email) 
                ?? throw new InvalidOperationException("User not found");
        }

        public static async Task<AppUser?> FindByEmailFromClaimsPrincipal(this UserManager<AppUser> userManager, 
            ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);

            if (email == null)
                throw new InvalidOperationException("Email claim not found.");

            return await userManager.Users.SingleOrDefaultAsync(x => x.Email == email);
        }

        public static string GetUsername(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value 
                   ?? throw new InvalidOperationException("Username claim not found.");
        }

        public static int GetUserId(this ClaimsPrincipal user)
        {
            var userIdValue = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdValue == null)
                throw new InvalidOperationException("User ID claim not found.");

            return int.Parse(userIdValue);
        }
    }
}
