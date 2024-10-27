using System.ComponentModel.DataAnnotations.Schema;
using API.DTOs;

namespace API.Entities
{
    public class Message : BaseEntity
    {
        public string? SenderId { get; set; }
        public List<string> RecipientIds { get; set; } = new List<string>();
        public string? Content { get; set; }
        public string? FileUrl { get; set; }
        public string? FileType { get; set; }
        public string? RepliedById { get; set; }
        public bool IsReplied { get; set; } = false;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        [ForeignKey("SenderId")]
        public AppUser? Sender { get; set; }
         [ForeignKey("RepliedById")]
        public AppUser? RepliedBy { get; set; }
        public static MessageDto toMessageDto(Message message){
            return new MessageDto{
                Id = message.Id,
                SenderId = message.SenderId,
                RecipientIds = message.RecipientIds,
                Content = message.Content,
                FileUrl = message.FileUrl,
                FileType = message.FileType,
                SentAt = message.SentAt,
                IsReplied = message.IsReplied,
                RepliedById = message.RepliedById,
            };
        }

    }
}