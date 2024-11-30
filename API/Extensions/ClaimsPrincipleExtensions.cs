using Shop.Domain.Exceptions;
using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new BadRequestException("Cannot get username from token");

            return userId;
        }
    }
}
