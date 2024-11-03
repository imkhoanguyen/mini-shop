namespace Shop.Application.DTOs.Messages
{
    public class MessageDto
    {
        public int Id { get; set; }

        public string? SenderId { get; set; }

        public string? RecipientId { get; set; }

        public string? Content { get; set; }

        public string? FileUrl { get; set; }

        public string? FileType { get; set; }

        public DateTime SentAt { get; set; }

        
    }
}