namespace API.DTOs
{
    public class EmailRequest
    {
        public required string To { get; set; }
        public required string Subject { get; set; }
        public required string Content { get; set; }
    }
}
