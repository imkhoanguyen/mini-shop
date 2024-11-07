using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Domain.Entities
{
    public class Message : BaseEntity
    {
        public string? SenderId { get; set; }
        public List<string>? RecipientIds { get; set; }
        public string? Content { get; set; }
        public string? FileUrl { get; set; }
        public string? FileType { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsReplied { get; set; } = false;
        public string? RepliedByUserId { get; set; }

        [ForeignKey("SenderId")]
        public AppUser? Sender { get; set; }
        [ForeignKey("RepliedByUserId ")]
        public AppUser? RepliedByUser { get; set; }

        public ICollection<MessageFile> Files { get; set; } = new List<MessageFile>();
    }
}