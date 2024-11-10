namespace Shop.Application.DTOs.Messages
{
    public class MessageBase
    {
        public string? SenderId { get; set; }
        public List<string>? RecipientIds { get; set; }
        public string? Content { get; set; }


    }
}
