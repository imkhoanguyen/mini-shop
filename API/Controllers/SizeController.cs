using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using API.DTOs;
using API.Entities;
using API.Helper;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using API.Repositories;

namespace API.Controllers
{
    public class SizeController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SizeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET api/size/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllSizesAsync()
        {
            var sizes = await _unitOfWork.SizeRepository.GetAllSizesAsync();
            return Ok(sizes);
        }
        // GET api/size/GetById
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetSizesById(int id)
        {
            var sizes = await _unitOfWork.SizeRepository.GetSizesById(id);
            return Ok(sizes);
        }

        [HttpGet("GetAllPaging")]
        public async Task<ActionResult<IEnumerable<SizeDto>>> GetAllsizesAsync([FromQuery] SizeParams sizeParams)
        {
            var sizes = await _unitOfWork.SizeRepository.GetAllSizesAsync(sizeParams);
            return Ok(sizes);
        }

        // POST api/size/Add
        [HttpPost("Add")]
        public async Task<ActionResult> Addsize(SizeDto sizeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (await _unitOfWork.SizeRepository.SizeExistsAsync(sizeDto.Name!))
            {
                return BadRequest("size với tên này đã tồn tại.");
            }
            var size = _mapper.Map<Size>(sizeDto);  // Map Dto to Entity
            _unitOfWork.SizeRepository.AddSize(size);

            if (await _unitOfWork.Complete())
                return NoContent();
            return Ok("Add size successfully.");
        }

        // PUT api/size/Update
        [HttpPut("Update")]
        public async Task<IActionResult> Updatesize(SizeDto sizeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (await _unitOfWork.SizeRepository.SizeExistsAsync(sizeDto.Name!))
            {
                return BadRequest("size với tên này đã tồn tại.");
            }
            var size = _mapper.Map<Size>(sizeDto);
            _unitOfWork.SizeRepository.UpdateSize(size);

            if (await _unitOfWork.Complete())
                return NoContent();

            return Ok("Update size successfully.");
        }

        // DELETE api/size/Delete
        [HttpDelete("Delete")]
        public async Task<IActionResult> Deletesize(SizeDto sizeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var size = _mapper.Map<Size>(sizeDto);
            _unitOfWork.SizeRepository.DeleteSize(size);

            if (await _unitOfWork.Complete())
                return NoContent();

            return Ok("Delete size successfully.");
        }
    }
}