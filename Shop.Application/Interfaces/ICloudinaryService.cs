using Microsoft.AspNetCore.Http;
using Shop.Application.DTOs.Cloudinary;

namespace Shop.Application.Interfaces
{
    public interface ICloudinaryService
    {
        Task<FileUploadResult> UploadPhotoAsync(IFormFile formFile);
        Task<FileDeleteResult> DeleteImageAsync(string publicId);
        Task<FileUploadResult> UploadImageAsync(IFormFile formFile);
        Task<FileUploadResult> UploadVideoAsync(IFormFile formFile);
        Task<FileUploadResult> UploadFileAsync(IFormFile formFile);

    }
}