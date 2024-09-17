using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser
    {
        public string? Fullname { get; set; }
        public Address? Address { get; set; }
    }
}
