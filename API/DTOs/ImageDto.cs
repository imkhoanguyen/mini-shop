using API.Entities;
using API.Interfaces;

namespace API.DTOs
{
    public class ImageUpdateDto
    {
        public int Id { get; set; }
        public IFormFile? Url { get; set; }
        public bool IsMain { get; set; }

        public static async Task<Image> toImage(ImageUpdateDto imageDto, ICloudinaryService imageService)
        {
            if (imageDto.Url != null)
            {
                var uploadResult = await imageService.UploadPhotoAsync(imageDto.Url);

                return new Image
                {
                    Id = imageDto.Id,
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
        public int VariantId { get; set; }
        public IFormFile? Url { get; set; }
        public bool IsMain { get; set; }

        public static async Task<Image> toImage(ImageAddDto imageAddDto, ICloudinaryService imageService)
        {
            if (imageAddDto.Url != null)
            {
                var uploadResult = await imageService.UploadPhotoAsync(imageAddDto.Url);

                return new Image
                {
                    VariantId = imageAddDto.VariantId,
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