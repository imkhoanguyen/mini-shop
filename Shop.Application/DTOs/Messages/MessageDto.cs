namespace Shop.Application.DTOs.Messages
{
    public class MessageDto : MessageBase
    {
        public int Id { get; set; }
        public DateTime SentAt { get; set; }
    }
}