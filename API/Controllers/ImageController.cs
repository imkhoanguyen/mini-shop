using API.DTOs;
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
                return BadRequest("Dữ liệu không hợp lệ.");
            }
            var image = await ImageAddDto.toImage(imageAddDto, imageService);
            _unitOfWork.ImageRepository.AddImage(image);
            if (await _unitOfWork.Complete())
            {
                return Ok("Add image successfully.");
            }
            return BadRequest("Add image failed.");
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
                return Ok("Update image successfully.");
            }
            return BadRequest("Update image failed.");
        }
    }
}