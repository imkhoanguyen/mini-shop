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
        public async Task<IActionResult> AddVariant([FromForm] VariantAddDto variantAddDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }
            var variant = VariantAddDto.toVariant(variantAddDto);
            _unitOfWork.VariantRepository.AddVariant(variant);
            if (await _unitOfWork.Complete())
            {
                return Ok("Add variant successfully.");
            }
            return BadRequest("Add variant failed.");
        }
        [HttpPost("UpdateVariant")]
        public async Task<IActionResult> UpdateVariant([FromForm] VariantDto variantDto)
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