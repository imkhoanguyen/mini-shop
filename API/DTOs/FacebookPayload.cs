using Newtonsoft.Json;

namespace API.DTOs
{
    public class PictureData
    {
        [JsonProperty("data")]
        public PictureDetails? Data { get; set; }
    }

    public class PictureDetails
    {
        public bool IsSilhouette { get; set; }
        public string? Url { get; set; }
    }

    public class FacebookPayload
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public PictureData? Picture { get; set; } 
    }
}