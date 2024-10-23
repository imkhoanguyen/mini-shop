using System.ComponentModel.DataAnnotations;
namespace API.DTOs
{
    public class RegisterDto
    {
        public required string Fullname { get; set; }
        public required string UserName { get; set; }
        
        public required string Password { get; set; }
        [EmailAddress]
        public required string? Email { get; set; }
    }
}