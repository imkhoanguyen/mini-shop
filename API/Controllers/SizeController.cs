using API.Entities;
using API.Helpers;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Repositories;

namespace API.Controllers
{
    public class SizeController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        //public SizeController(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}

        //// GET api/size/GetAll
        //[HttpGet("GetAll")]
        //public async Task<IActionResult> GetAllSizesAsync()
        //{
        //    var sizes = await _unitOfWork.SizeRepository.GetAllSizesAsync();
        //    if(sizes == null)
        //    {
        //        return NotFound("Không tìm thấy size nào.");
        //    }
        //    var sizesDto = sizes.Select(s => Size.toSizeDto(s)).ToList();
        //    return Ok(sizesDto);
        //}
        //// GET api/size/GetById
        //[HttpGet("GetById/{id}")]
        //public async Task<IActionResult> GetSizesById(int id)
        //{
        //    var sizes = await _unitOfWork.SizeRepository.GetSizesById(id);
        //    if(sizes == null)
        //    {
        //        return NotFound("Không tìm thấy size nào.");
        //    }
        //    var sizesDto = Size.toSizeDto(sizes);
        //    return Ok(sizesDto);
        //}

        //[HttpGet("GetAllPaging")]
        //public async Task<ActionResult<IEnumerable<SizeDto>>> GetAllsizesAsync([FromQuery] SizeParams sizeParams)
        //{
        //    var sizes = await _unitOfWork.SizeRepository.GetAllSizesAsync(sizeParams);
        //    if(sizes == null)
        //    {
        //        return NotFound("Không tìm thấy size nào.");
        //    }
            
        //    var sizesDto = sizes.Select(s => Size.toSizeDto(s)).ToList();
        //    return Ok(sizesDto);
        //}

        //// POST api/size/Add
        //[HttpPost("Add")]
        //public async Task<ActionResult> Addsize(SizeAddDto sizeAddDto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    if (await _unitOfWork.SizeRepository.SizeExistsAsync(sizeAddDto.Name!))
        //    {
        //        return BadRequest(new {message = "Size với tên này đã tồn tại."});
        //    }
        //    var size = SizeAddDto.toSize(sizeAddDto);
        //    _unitOfWork.SizeRepository.AddSize(size);

        //    if (await _unitOfWork.Complete())
        //        return Ok(new {message ="Thêm kích thước thành công"});
        //    return BadRequest(new {message ="Thêm kích thước thất bại"});

        //}

        //// PUT api/size/Update
        //[HttpPut("Update")]
        //public async Task<IActionResult> Updatesize(SizeDto sizeDto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    if (await _unitOfWork.SizeRepository.SizeExistsAsync(sizeDto.Name!))
        //    {
        //        return BadRequest(new {message ="size với tên này đã tồn tại."});
        //    }
        //    var size = SizeDto.toSize(sizeDto);
        //    _unitOfWork.SizeRepository.UpdateSize(size);

        //    if (await _unitOfWork.Complete())
        //        return Ok(new {message ="Cập nhật kích thước thành công."});
        //    return BadRequest(new {message ="Cập nhật kích thước thất bại."});

        //}

        //// DELETE api/size/Delete
        //[HttpDelete("Delete")]
        //public async Task<IActionResult> Deletesize(SizeDto sizeDto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    var size = SizeDto.toSize(sizeDto);
        //    _unitOfWork.SizeRepository.DeleteSize(size);

        //    if (await _unitOfWork.Complete())
        //        return Ok("Delete size successfully.");
        //    return BadRequest("Delete size failed.");

        //}
    }
}