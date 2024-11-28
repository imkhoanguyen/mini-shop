using Microsoft.AspNetCore.Identity;

namespace Shop.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;
        // public Address? Address { get; set; }
        public List<Address> Address { get; set; } = new();
        public string? Avatar { get; set; } = "user.jpg";
        public List<Image> Images { get; set; } = new();
        public List<Message> MessageSent { get; set; } = new();
        public List<Message> MessageReceived { get; set; } = new();

        
    }
}
