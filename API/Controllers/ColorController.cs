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

namespace API.Controllers
{
    public class ColorController : BaseApiController
    {
         private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ColorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET api/color/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllcolorsAsync()
        {
            var colors = await _unitOfWork.colorRepository.GetAllColorsAsync();
            return Ok(colors);
        }
        // GET api/color/GetById
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetcolorsById(int id)
        {
            var colors = await _unitOfWork.colorRepository.GetColorsById(id);
            return Ok(colors);
        }

        [HttpGet("GetAllPaging")]
        public async Task<ActionResult<IEnumerable<ColorDto>>> GetAllcolorsAsync([FromQuery] ColorParams colorParams)
        {
            var colors = await _unitOfWork.colorRepository.GetAllColorsAsync(colorParams);
            return Ok(colors);
        }

        // POST api/color/Add
        [HttpPost("Add")]
        public async Task<ActionResult> Addcolor(ColorDto colorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (await _unitOfWork.colorRepository.colorExistsAsync(colorDto.Name!))
            {
                return BadRequest("color với tên này đã tồn tại.");
            }
            var color = _mapper.Map<Color>(colorDto);  // Map Dto to Entity
            _unitOfWork.colorRepository.AddColor(color);

            if (await _unitOfWork.Complete())
                return NoContent();
            return Ok("Add color successfully.");
        }

        // PUT api/color/Update
        [HttpPut("Update")]
        public async Task<IActionResult> Updatecolor(ColorDto colorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (await _unitOfWork.colorRepository.colorExistsAsync(colorDto.Name!))
            {
                return BadRequest("color với tên này đã tồn tại.");
            }
            var color = _mapper.Map<Color>(colorDto);
            _unitOfWork.colorRepository.UpdateColor(color);

            if (await _unitOfWork.Complete())
                return NoContent();

            return Ok("Update color successfully.");
        }

        // DELETE api/color/Delete
        [HttpDelete("Delete")]
        public async Task<IActionResult> Deletecolor(ColorDto colorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var color = _mapper.Map<Color>(colorDto);
            _unitOfWork.colorRepository.DeleteColor(color);

            if (await _unitOfWork.Complete())
                return NoContent();

            return Ok("Delete color successfully.");
        }
    }
}