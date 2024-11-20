
using Microsoft.AspNetCore.Mvc;
using Shop.Application.DTOs.Colors;
using Shop.Application.Mappers;
using Shop.Application.Parameters;
using Shop.Application.Services.Abstracts;

namespace API.Controllers
{
    public class ColorController : BaseApiController
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
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

            return Ok(new { message = "Thêm màu thanh cong" });
        }

        // PUT api/color/Update
        [HttpPut("Update")]
        public async Task<IActionResult> Updatecolor(ColorDto colorDto)
        {
            var color = ColorMapper.ColorDtoToEntity(colorDto);
            await _colorService.UpdateAsync(color);

            return Ok("Update color success.");
        }

        // DELETE api/color/Delete
        [HttpDelete("Delete")]
        public async Task<IActionResult> Deletecolor(ColorDto colorDto)
        {
            var color = ColorMapper.ColorDtoToEntity(colorDto);
            await _colorService.DeleteAsync(color);

            return Ok("Delete color success.");

        }
    }
}