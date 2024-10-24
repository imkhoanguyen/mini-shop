using CloudinaryDotNet.Actions;

namespace API.Interfaces
{
    public interface IImageService
    {
        Task<ImageUploadResult> UploadImageAsync(IFormFile formFile);
        Task<UploadResult> UploadFileAsync(IFormFile file);
        Task<DeletionResult> DeleteImageAsync(string publicId);
    }
}