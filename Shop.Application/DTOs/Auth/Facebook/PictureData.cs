using Newtonsoft.Json;

namespace Shop.Application.DTOs.Auth.Facebook
{
    public class PictureData
    {
        [JsonProperty("data")]
        public PictureDetails? Data { get; set; }
    }
}
