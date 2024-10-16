using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class ResetPasswordDto
    {
        public required string Email { get; set; }
        public required string Token { get; set; }
        public required string Password { get; set; }
    }
}
