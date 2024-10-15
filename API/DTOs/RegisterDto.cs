using System.ComponentModel.DataAnnotations;
namespace API.DTOs
{
    public class RegisterDto
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        [EmailAddress]
        public string? Email { get; set; }
    }
}