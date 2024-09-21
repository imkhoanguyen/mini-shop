using API.Entities;

namespace API.DTOs
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string? SenderId { get; set; }
        public string? SenderUserName { get; set; }
        public string? SenderUrl { get; set; }
        public string? RecipientId { get; set; }
        public string? RecipientUserName { get; set; }
        public string? RecipientUrl { get; set; }
        public string? Content { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSend { get; set; }

        public static Message toMessage(MessageDto messageDto)
        {
            return new Message
            {
                Id = messageDto.Id,
                SenderId = messageDto.SenderId,
                SenderUserName = messageDto.SenderUserName,
                RecipientId = messageDto.RecipientId,
                RecipientUserName = messageDto.RecipientUserName,
                Content = messageDto.Content,
                DateRead = messageDto.DateRead ?? DateTime.UtcNow,
                MessageSend = messageDto.MessageSend
            };
        }
    }
}