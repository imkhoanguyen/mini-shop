using System.ComponentModel.DataAnnotations;

namespace Shop.Application.DTOs.Auth
{
    public class ResetPasswordDto
    {
        public required string Email { get; set; }
        public required string Token { get; set; }
        public required string Password { get; set; }
    }
}
