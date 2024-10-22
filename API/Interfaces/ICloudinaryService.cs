using CloudinaryDotNet.Actions;

namespace API.Interfaces
{
    public interface ICloudinaryService
    {
        Task<ImageUploadResult> UploadPhotoAsync(IFormFile formFile);
        Task<DeletionResult> DeleteImageAsync(string publicId);
        Task<ImageUploadResult> UploadImageAsync(IFormFile formFile);
        Task<VideoUploadResult> UploadVideoAsync(IFormFile formFile);
        
    }
}