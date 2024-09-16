using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace API.Services
{
    public class ImageService : IImageService
    {
        private readonly Cloudinary _cloudinary;
        public ImageService(Cloudinary cloudinary){
            _cloudinary = cloudinary;
        }
        public async Task<ImageUploadResult> UploadImageAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if(file.Length>0){
                using var stream = file.OpenReadStream();
                var uploadParam = new ImageUploadParams{
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill")
                };
                uploadResult =await _cloudinary.UploadAsync(uploadParam);
            }
            return uploadResult;
        }

        public async Task<DeletionResult> DeleteImageAsync(string publicId)
        {
            var deleteParam = new DeletionParams(publicId);
            return await _cloudinary.DestroyAsync(deleteParam);
        }
    }
}