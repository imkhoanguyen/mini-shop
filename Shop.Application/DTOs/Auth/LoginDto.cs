using System.ComponentModel.DataAnnotations;

namespace Shop.Application.DTOs.Auth
{
    public class LoginDto
    {
        [Required]
        public string? UserNameOrEmail { get; set; }
        [Required]
        public string Password { get; set; } = null!;
    }

}