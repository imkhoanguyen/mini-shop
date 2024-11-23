using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Domain.Entities
{
    public class Message : BaseEntity
    {
        public string? SenderId { get; set; }
        public List<string>? RecipientIds { get; set; }
        public string? Content { get; set; }
        public DateTime SentAt { get; set; }
        public string? RepliedByAdminId { get; set; }

        [ForeignKey("SenderId")]
        public AppUser? Sender { get; set; }
        [ForeignKey("RepliedByAdminId ")]
        public AppUser? RepliedByAdmin { get; set; }

        public ICollection<MessageFile> Files { get; set; } = new List<MessageFile>();
    }
}