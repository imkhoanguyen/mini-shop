using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace API.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        public CloudinaryService(Cloudinary cloudinary){
            _cloudinary = cloudinary;
        }
        public async Task<ImageUploadResult> UploadPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if(file.Length>0){
                using var stream = file.OpenReadStream();
                var uploadParam = new ImageUploadParams{
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
                    Folder= "mini-upload"
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

        public async Task<ImageUploadResult> UploadImageAsync(IFormFile formFile)
        {
            var uploadResult = new ImageUploadResult();
            if (formFile.Length > 0)
            {
                using var stream = formFile.OpenReadStream();
                var uploadParam = new ImageUploadParams
                {
                    File = new FileDescription(formFile.FileName, stream),
                    Folder = "mini-upload"
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParam);
            }
            return uploadResult;
        }

        public async Task<VideoUploadResult> UploadVideoAsync(IFormFile formFile)
        {
            var uploadResult = new VideoUploadResult();
            if (formFile.Length > 0)
            {
                using var stream = formFile.OpenReadStream();
                var uploadParam = new VideoUploadParams
                {
                    File = new FileDescription(formFile.FileName, stream),
                    Folder = "mini-upload",  
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParam);
            }
            return uploadResult;
        }

    }
}