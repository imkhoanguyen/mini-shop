using System.ComponentModel.DataAnnotations;
namespace Shop.Application.DTOs.Auth
{
    public class RegisterDto
    {
        public required string FullName { get; set; }
        public required string UserName { get; set; }

        public required string Password { get; set; }
        [EmailAddress]
        public required string? Email { get; set; }
    }
}