using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.DTOs.Colors;
using Shop.Application.Mappers;
using Shop.Application.Services.Implementations;

namespace API.Controllers
{
    public class ColorController : BaseApiController
    {
        private readonly ColorService _colorService;

        public ColorController(ColorService colorService)
        {
            _colorService = colorService;
        }

        // GET api/color/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllcolorsAsync()
        {
            var colors = await _colorService.GetAllColorsAsync();

            var colorsDto = colors.Select(c => ColorMapper.EntityToColorDto(c)).ToList();
            return Ok(colorsDto);
        }
        // GET api/color/GetById
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetcolorsById(int id)
        {
            var color = await _colorService.GetColorsById(id);
            var colorDto = ColorMapper.EntityToColorDto(color);
            return Ok(colorDto);
        }

        [HttpGet("GetAllPaging")]
        public async Task<ActionResult<IEnumerable<ColorDto>>> GetAllcolorsAsync([FromQuery] ColorParams colorParams)
        {
            var colors = await _colorService.GetAllColorsAsync(colorParams);
            if (colors == null)
            {
                return NotFound("Không tìm thấy color nào.");
            }
            var colorsDto = colors.Select(c => ColorMapper.EntityToColorDto(c)).ToList();
            return Ok(colors);
        }

        // POST api/color/Add
        [HttpPost("Add")]
        public async Task<ActionResult> Addcolor(ColorAddDto colorAddDto)
        {
            var color = ColorMapper.ColorAddDtoToEntity(colorAddDto);
            await _colorService.AddColor(color);

            return BadRequest(new { message = "Thêm màu sắc thất bại" });
        }

        // PUT api/color/Update
        [HttpPut("Update")]
        public async Task<IActionResult> Updatecolor(ColorDto colorDto)
        {
            var color = ColorMapper.ColorDtoToEntity(colorDto);
            await _colorService.UpdateAsync(color);

            if (await _colorService.CompleteAsync())
                return Ok("Update color successfully.");
            return BadRequest("Update color failed.");
        }

        // DELETE api/color/Delete
        [HttpDelete("Delete")]
        public async Task<IActionResult> Deletecolor(ColorDto colorDto)
        {
            var color = ColorMapper.ColorDtoToEntity(colorDto);
            await _colorService.DeleteAsync(color);

            if (await _colorService.CompleteAsync())
                return Ok("Delete color successfully.");
            return BadRequest("Delete color failed.");

        }
    }
}