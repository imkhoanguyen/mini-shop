using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppRole : IdentityRole
    {
        public string? Description { get; set; }
    }
}
