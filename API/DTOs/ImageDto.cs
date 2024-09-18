using API.Entities;

namespace API.DTOs
{
    public class ImageDto
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public bool IsMain { get; set; }

        public static Image toImage(ImageDto imageDto)
        {
            return new Image
            {
                Id = imageDto.Id,
                Url = imageDto.Url!,
                IsMain = imageDto.IsMain
            };
        }
    }
}