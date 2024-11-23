using API.Helpers;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Shop.Application.DTOs.Cloudinary;
using Shop.Application.Services.Abstracts;

namespace Shop.Infrastructure.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        public CloudinaryService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret);
            _cloudinary = new Cloudinary(acc);
        }
        public async Task<FileUploadResult> UploadPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParam = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(300).Width(450).Crop("fill").Gravity("face"),
                    Folder = "mini-upload"
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParam);
            }
            if (uploadResult.Error != null)
            {
                return new FileUploadResult
                {
                    Error = uploadResult.Error.Message,
                };
            }

            return new FileUploadResult
            {
                PublicId = uploadResult.PublicId,
                Url = uploadResult.SecureUrl.ToString(),
                Error = null
            };
        }

        public async Task<FileDeleteResult> DeleteImageAsync(string publicId)
        {
            var deleteParam = new DeletionParams(publicId);
            var result =  await _cloudinary.DestroyAsync(deleteParam);
            if (result.Error != null)
            {
                return new FileDeleteResult
                {
                    Error = result.Error.Message
                };
            }


            return new FileDeleteResult
            {
                Result = result.Result,
                Error = null
            };
        }

        public async Task<FileUploadResult> UploadImageAsync(IFormFile formFile)
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
            if (uploadResult.Error != null)
            {
                return new FileUploadResult
                {
                    Error = uploadResult.Error.Message,
                };
            }

            return new FileUploadResult
            {
                PublicId = uploadResult.PublicId,
                Url = uploadResult.SecureUrl.ToString(),
                Error = null
            };
        }

        public async Task<FileUploadResult> UploadVideoAsync(IFormFile formFile)
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
            if (uploadResult.Error != null)
            {
                return new FileUploadResult
                {
                    Error = uploadResult.Error.Message,
                };
            }

            return new FileUploadResult
            {
                PublicId = uploadResult.PublicId,
                Url = uploadResult.SecureUrl.ToString(),
                Error = null
            };
        }


        public async Task<FileUploadResult> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File cannot null or empty");

            using var stream = file.OpenReadStream();
            var upLoadParams = new RawUploadParams()
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "mini-shop",
                PublicId = Guid.NewGuid().ToString()
            };

            if (file.ContentType.StartsWith("image"))
            {
                var imageUploadParam = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = "mini-images",
                    PublicId = Guid.NewGuid().ToString(),
                    Transformation = new Transformation().Height(300).Width(450).Crop("fill").Gravity("face"),
                };
                var imageResult = await _cloudinary.UploadAsync(imageUploadParam);
                if (imageResult.Error != null)
                {
                    return new FileUploadResult
                    {
                        Error = imageResult.Error.Message,
                    };
                }

                return new FileUploadResult
                {
                    PublicId = imageResult.PublicId,
                    Url = imageResult.SecureUrl.ToString(),
                    Error = null
                };
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
                if (videoResult.Error != null)
                {
                    return new FileUploadResult
                    {
                        Error = videoResult.Error.Message,
                    };
                }

                return new FileUploadResult
                {
                    PublicId = videoResult.PublicId,
                    Url = videoResult.SecureUrl.ToString(),
                    Error = null
                };
            }
            else
            {
                var rawResult = await _cloudinary.UploadAsync(upLoadParams);
                if (rawResult.Error != null)
                {
                    return new FileUploadResult
                    {
                        Error = rawResult.Error.Message,
                    };
                }

                return new FileUploadResult
                {
                    PublicId = rawResult.PublicId,
                    Url = rawResult.SecureUrl.ToString(),
                    Error = null
                };
            }
        }
    }
}