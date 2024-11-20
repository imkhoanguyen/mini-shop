using API.Helpers;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.DTOs.Size;
using Shop.Application.Mappers;
using Shop.Application.Repositories;
using Shop.Application.Services.Abstracts;
using Shop.Application.Services.Implementations;

namespace API.Controllers
{
    public class SizeController : BaseApiController
    {
        private readonly ISizeService _sizeService;

        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        // GET api/size/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllSizesAsync()
        {
            var sizes = await _sizeService.GetAllSizesAsync();
            var sizesDto = sizes.Select(s => SizeMapper.EntityToSizeDto(s)).ToList();
            return Ok(sizesDto);
        }
        // GET api/size/GetById
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetSizesById(int id)
        {
            var size = await _sizeService.GetSizesById(id);
            return Ok(SizeMapper.EntityToSizeDto(size));
        }

        [HttpGet("GetAllPaging")]
        public async Task<ActionResult<IEnumerable<SizeDto>>> GetAllSizesAsync([FromQuery] SizeParams sizeParams)
        {
            var sizes = await _sizeService.GetAllSizesAsync(sizeParams);

            var sizesDto = sizes.Select(s => SizeMapper.EntityToSizeDto(s)).ToList();
            return Ok(sizesDto);
        }

        // POST api/size/Add
        [HttpPost("Add")]
        public async Task<ActionResult> Addsize(SizeAddDto sizeAddDto)
        {
            var size = SizeMapper.SizeAddDtoToEntity(sizeAddDto);
            await _sizeService.AddSize(size);

            return Ok(new { message = "Thêm kích thước thành công" });

        }

        // PUT api/size/Update
        [HttpPut("Update")]
        public async Task<IActionResult> Updatesize(SizeDto sizeDto)
        {
            var size = SizeMapper.SizeDtoToEntity(sizeDto);
            await _sizeService.UpdateAsync(size);

            return Ok(new { message = "Cập nhật kích thước hành công." });

        }

        // DELETE api/size/Delete
        [HttpDelete("Delete")]
        public async Task<IActionResult> Deletesize(SizeDto sizeDto)
        {
            var size = SizeMapper.SizeDtoToEntity(sizeDto);
            await _sizeService.DeleteAsync(size);

            return BadRequest("Delete size success.");

        }
    }
}