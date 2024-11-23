using Microsoft.AspNetCore.Http;

namespace Shop.Application.DTOs.Messages
{
    public class MessageAdd : MessageBase
    {
        public List<IFormFile> Files { get; set; } =  new List<IFormFile>();
    }
}
