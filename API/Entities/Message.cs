using API.DTOs;

namespace API.Entities
{
    public class Message : BaseEntity
    {
        public string? SenderId { get; set; }
        public string? SenderUserName { get; set; }
        public AppUser? Sender { get; set; }
        public string? RecipientId { get; set; }
        public string? RecipientUserName { get; set; }
        public AppUser? Recipient { get; set; }
        public string? Content { get; set; }
        public DateTime DateRead { get; set; }
        public DateTime MessageSend { get; set; } = DateTime.UtcNow;

        public static MessageDto toMessageDto(Message message)
        {
            return new MessageDto
            {
                Id = message.Id,
                SenderId = message.SenderId,
                SenderUserName = message.SenderUserName,
                RecipientId = message.RecipientId,
                RecipientUserName = message.RecipientUserName,
                Content = message.Content,
                DateRead = message.DateRead,
                MessageSend = message.MessageSend
                
            };
        }

    }
}