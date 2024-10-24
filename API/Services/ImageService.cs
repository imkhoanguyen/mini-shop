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
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
                    Folder= "mini-upload"
                };
                uploadResult =await _cloudinary.UploadAsync(uploadParam);
            }
            return uploadResult;
        }
        public async Task<UploadResult> UploadFileAsync(IFormFile file)
        {
            if(file == null || file.Length ==0) 
                throw new ArgumentException("File cannot null or empty");

            using var stream = file.OpenReadStream();
            var upLoadParams = new RawUploadParams(){
                File = new FileDescription(file.FileName, stream),
                Folder = "mini-shop",
                PublicId = Guid.NewGuid().ToString()
            };

            if(file.ContentType.StartsWith("image")){
                var imageUploadParam = new ImageUploadParams{
                    File = new FileDescription(file.FileName, stream),
                    Folder = "mini-images",
                    PublicId = Guid.NewGuid().ToString(),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
                };
                var imageResult = await _cloudinary.UploadAsync(imageUploadParam);
                return imageResult;
            }
            else if (file.ContentType.StartsWith("video"))
            {
                var videoUploadParams = new VideoUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = "mini-videos",
                    PublicId = Guid.NewGuid().ToString(),
                    Transformation = new Transformation().Quality("auto")
                };
                var videoResult = await _cloudinary.UploadAsync(videoUploadParams);
                return videoResult;
            }
            else
            {
                var rawResult = await _cloudinary.UploadAsync(upLoadParams);
                return rawResult;
            }
        }


        public async Task<DeletionResult> DeleteImageAsync(string publicId)
        {
            var deleteParam = new DeletionParams(publicId);
            return await _cloudinary.DestroyAsync(deleteParam);
        }
    }
}