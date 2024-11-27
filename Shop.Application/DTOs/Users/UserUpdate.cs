using Microsoft.AspNetCore.Http;

namespace Shop.Application.DTOs.Users
{
    public class UserUpdate
    {
        public string? Id { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public IFormFile? Avatar { get; set; }
    }
}
