using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.Controllers{
    public class CartItemsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartItemsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork =unitOfWork;
        }
        [HttpPost("AddCartItems")]
        public async Task<IActionResult> AddCartItems([FromForm]CartItemsAddDto cartItemsAddDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Dữ liệu không hợp lệ!");
            }
            var cartItems =CartItemsAddDto.toCartItems(cartItemsAddDto);
            _unitOfWork.CartItemsRepository.AddCartItems(cartItems);
            if (await _unitOfWork.Complete())
            {
                return Ok("Thêm CartItems thành công!");
            }
            return BadRequest("Thêm CartItems không thành công!");
        }
        [HttpPost("UpdateCartItems")]
        public async Task<IActionResult> UpdateCartItems([FromForm]CartItemsUpdateDto cartItemsUpdateDto)
        {
                if(!ModelState.IsValid)
                {
                    return BadRequest("Dữ liệu không hợp lệ!");
                }
                var cartItems=CartItemsUpdateDto.toCartItems(cartItemsUpdateDto);
                _unitOfWork.CartItemsRepository.UpdateCartItems(cartItems);
                if(await _unitOfWork.Complete())
                {
                    return Ok("Them thanh cong");
                }
                return BadRequest("them that bai");
        }
        
    }
}