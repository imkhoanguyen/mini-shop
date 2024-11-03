using Shop.Application.Parameters;

namespace API.Helpers
{
    public class MessageParams : BaseParams
    {
        public string? UserName { get; set; }
        public string Container { get; set; } = "Unread";
    }
}