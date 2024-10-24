using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ColorController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ColorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET api/color/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllcolorsAsync()
        {
            var colors = await _unitOfWork.ColorRepository.GetAllColorsAsync();
            if (colors == null)
            {
                return NotFound("Không tìm thấy color nào.");
            }
            var colorsDto = colors.Select(c => Color.toColorDto(c)).ToList();
            return Ok(colorsDto);
        }
        // GET api/color/GetById
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetcolorsById(int id)
        {
            var colors = await _unitOfWork.ColorRepository.GetColorsById(id);
            if (colors == null)
            {
                return NotFound("Không tìm thấy color nào.");
            }
            var colorDto = Color.toColorDto(colors);
            return Ok(colorDto);
        }

        [HttpGet("GetAllPaging")]
        public async Task<ActionResult<IEnumerable<ColorDto>>> GetAllcolorsAsync([FromQuery] ColorParams colorParams)
        {
            var colors = await _unitOfWork.ColorRepository.GetAllColorsAsync(colorParams);
            if (colors == null)
            {
                return NotFound("Không tìm thấy color nào.");
            }
            var colorsDto = colors.Select(c => Color.toColorDto(c)).ToList();
            return Ok(colors);
        }

        // POST api/color/Add
        [HttpPost("Add")]
        public async Task<ActionResult> Addcolor(ColorAddDto colorAddDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (await _unitOfWork.ColorRepository.colorExistsAsync(colorAddDto.Name!))
            {
                return BadRequest(new {message ="color với tên này đã tồn tại."});
            }
            var color = ColorAddDto.toColor(colorAddDto);
            _unitOfWork.ColorRepository.AddColor(color);

            if (await _unitOfWork.Complete())
                return Ok(new {message ="Thêm màu sắc thành công"});
            return BadRequest(new {message ="Thêm màu sắc thất bại"});
        }

        // PUT api/color/Update
        [HttpPut("Update")]
        public async Task<IActionResult> Updatecolor(ColorDto colorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (await _unitOfWork.ColorRepository.colorExistsAsync(colorDto.Name!))
            {
                return BadRequest("color với tên này đã tồn tại.");
            }
            var color = ColorDto.toColor(colorDto);
            _unitOfWork.ColorRepository.UpdateColor(color);

            if (await _unitOfWork.Complete())
                return Ok("Update color successfully.");
            return BadRequest("Update color failed.");
        }

        // DELETE api/color/Delete
        [HttpDelete("Delete")]
        public async Task<IActionResult> Deletecolor(ColorDto colorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var color = ColorDto.toColor(colorDto);
            _unitOfWork.ColorRepository.DeleteColor(color);

            if (await _unitOfWork.Complete())
                return Ok("Delete color successfully.");
            return BadRequest("Delete color failed.");

        }
    }
}