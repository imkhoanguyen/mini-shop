using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class VariantController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        public VariantController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost("AddVariant")]
        public async Task<IActionResult> AddVariant([FromBody] VariantAddDto variantAddDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new {message ="Dữ liệu không hợp lệ."});
            }
            var variant = VariantAddDto.toVariant(variantAddDto);
            _unitOfWork.VariantRepository.AddVariant(variant);
            if (await _unitOfWork.Complete())
            {
                return Ok(new {id = variant.Id, message ="Add variant successfully."});
            }
            return BadRequest(new {message ="Add variant failed."});
        }
        [HttpPost("UpdateVariant")]
        public async Task<IActionResult> UpdateVariant([FromBody] VariantDto variantDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }
            var variant = VariantDto.toVariant(variantDto);
            _unitOfWork.VariantRepository.UpdateVariant(variant);
            if (await _unitOfWork.Complete())
            {
                return Ok("Update variant successfully.");
            }
            return BadRequest("Update variant failed.");
        }
    }
}