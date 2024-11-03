using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.DTOs.Size;
using Shop.Application.Mappers;
using Shop.Application.Repositories;
using Shop.Application.Services.Implementations;

namespace API.Controllers
{
    public class SizeController : BaseApiController
    {
        private readonly SizeService _sizeService;

        public SizeController(SizeService sizeService)
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

            if (await _sizeService.CompleteAsync())
                return Ok(new { message = "Thêm kích thước thành công" });
            return BadRequest(new { message = "Thêm kích thước thất bại" });

        }

        // PUT api/size/Update
        [HttpPut("Update")]
        public async Task<IActionResult> Updatesize(SizeDto sizeDto)
        {
            var size = SizeMapper.SizeDtoToEntity(sizeDto);
            await _sizeService.UpdateAsync(size);

            if (await _sizeService.CompleteAsync())
                return Ok(new { message = "Cập nhật kích thước thành công." });
            return BadRequest(new { message = "Cập nhật kích thước thất bại." });

        }

        // DELETE api/size/Delete
        [HttpDelete("Delete")]
        public async Task<IActionResult> Deletesize(SizeDto sizeDto)
        {
            var size = SizeMapper.SizeDtoToEntity(sizeDto);
            await _sizeService.DeleteAsync(size);

            if (await _sizeService.CompleteAsync())
                return Ok("Delete size successfully.");
            return BadRequest("Delete size failed.");

        }
    }
}