using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ImageController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        public ImageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost("AddImage")]
        public async Task<IActionResult> AddImage([FromForm] ImageAddDto imageAddDto, IImageService imageService)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ." });
            }
            var image = await ImageAddDto.toImage(imageAddDto, imageService);
            _unitOfWork.ImageRepository.AddImage(image);
            if (await _unitOfWork.Complete())
            {
                return Ok(new { message = "Add image successfully." });
            }
            return BadRequest(new { message = "Add image failed." });
        }
        [HttpPut("UpdateImages")]
        public async Task<IActionResult> UpdateImages([FromForm] ImageUpdateDto imageUpdateDto, IImageService imageService)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }
            var image = await ImageUpdateDto.toImage(imageUpdateDto, imageService);
            _unitOfWork.ImageRepository.UpdateImage(image);
            if (await _unitOfWork.Complete())
            {
                return Ok(new { message = "Update image successfully." });
            }
            return BadRequest(new { message = "Update image failed." });
        }
        [HttpDelete("RemoveImage")]
        public async Task<IActionResult> RemoveImage(ImageUpdateDto imageUpdateDto)
        {
            var image = await _unitOfWork.ImageRepository.GetImageById(imageUpdateDto.Id);
            if (image is null)
            {
                return BadRequest(new { message = "Image not found." });
            }
            var deleteResult = await _unitOfWork.ImageService.DeleteImageAsync(image.PublicId);
            if (deleteResult.Result == "ok")
            {
                _unitOfWork.ImageRepository.RemoveImage(image);
                if (await _unitOfWork.Complete())
                {
                    return Ok(new { message = "Remove image successfully." });
                }
                else
                {
                    return BadRequest(new { message = "Failed to save changes to the database." });
                }
            }
            return BadRequest(new { message = "Remove image failed on Cloudinary." });
        }

    }
}