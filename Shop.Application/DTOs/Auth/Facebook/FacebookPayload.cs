using Newtonsoft.Json;

namespace Shop.Application.DTOs.Auth.Facebook
{
    public class FacebookPayload
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public PictureData? Picture { get; set; }
    }
}