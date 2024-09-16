using CloudinaryDotNet.Actions;

namespace API.Interfaces
{
    public interface IImageService
    {
        Task<ImageUploadResult> UploadImageAsync(IFormFile formFile);
        Task<DeletionResult> DeleteImageAsync(string publicId);
    }
}