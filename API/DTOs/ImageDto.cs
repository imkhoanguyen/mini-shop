using API.Entities;
using API.Interfaces;

namespace API.DTOs
{
    public class ImageUpdateDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public IFormFile? Url { get; set; }
        public bool IsMain { get; set; }

        public static async Task<Image> toImage(ImageUpdateDto imageDto, IImageService imageService)
        {
            if (imageDto.Url != null)
            {
                var uploadResult = await imageService.UploadImageAsync(imageDto.Url);

                return new Image
                {
                    Id = imageDto.Id,
                    ProductId = imageDto.ProductId,
                    Url = uploadResult.Url.ToString(),
                    PublicId = uploadResult.PublicId,
                    IsMain = imageDto.IsMain
                };
            }
            throw new Exception("No image provided.");
        }
    }
    public class ImageAddDto
    {
        public int ProductId { get; set; }
        public IFormFile? Url { get; set; }
        public bool IsMain { get; set; }

        public static async Task<Image> toImage(ImageAddDto imageAddDto, IImageService imageService)
        {
            if (imageAddDto.Url != null)
            {
                var uploadResult = await imageService.UploadImageAsync(imageAddDto.Url);

                return new Image
                {
                    ProductId = imageAddDto.ProductId,
                    Url = uploadResult.Url.ToString(),
                    PublicId = uploadResult.PublicId,
                    IsMain = imageAddDto.IsMain
                };
            }
            throw new Exception("No image provided.");
        }
    }
    public class ImageGetDto
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public bool IsMain { get; set; }
    }
}