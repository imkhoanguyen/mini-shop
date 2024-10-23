using System.ComponentModel.DataAnnotations.Schema;
using API.DTOs;

namespace API.Entities
{
    public class Message : BaseEntity
    {
        public string? SenderId { get; set; }
        public string? RecipientId { get; set; }
        public string? Content { get; set; }
        public string? FileUrl { get; set; }
        public string? FileType { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        [ForeignKey("SenderId")]
        public AppUser? Sender { get; set; }
        [ForeignKey("RecipientId")]
        public AppUser? Recipient { get; set; }
        public static MessageDto toMessageDto(Message message){
            return new MessageDto{
                Id = message.Id,
                SenderId = message.SenderId,
                RecipientId = message.RecipientId,
                Content = message.Content,
                FileUrl = message.FileUrl,
                FileType = message.FileType,
                SentAt = message.SentAt,
            };
        }

    }
}