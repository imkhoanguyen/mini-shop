namespace Shop.Application.DTOs.Messages
{
    public class MessageDto : MessageBase
    {
        public int Id { get; set; }
        public DateTime SentAt { get; set; }
        public List<FileMessageDto> Files { get; set; } = new List<FileMessageDto>();
    }
}