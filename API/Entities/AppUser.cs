using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser
    {
        public string? Fullname { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; }
        public Address? Address { get; set; }
        public List<Image> Images { get; set; } = new();
        public List<Message> MessageSent { get; set; } = new();
        public List<Message> MessageReceived { get; set; } = new();
    }
}
