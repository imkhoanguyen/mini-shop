using Microsoft.AspNetCore.Http;

namespace Shop.Application.DTOs.Users
{
    public class UserAdd
    {
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Role { get; set; }
        public IFormFile? Avatar { get; set; }
    }
}
