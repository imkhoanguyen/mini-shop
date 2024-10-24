using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ShoppingCartController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork =unitOfWork;
        }

        [HttpPost("AddShoppingCart")]
        public async Task<IActionResult> AddShoppongCart([FromForm] ShoppingCartAddDto shoppingCartAddDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var shoppingCart=ShoppingCartAddDto.toShoppingCart(shoppingCartAddDto);
            _unitOfWork.ShoppingCartRepository.AddShoppingCart(shoppingCart);
            if (await _unitOfWork.Complete())
            {
                return Ok(new{id=shoppingCart.Id,message="Thêm shoppingCart thành công!"});
                //await _unitOfWork.ShoppingCartRepository.AddShoppingCart(shoppingCart);
            }
            return BadRequest(new {message="Thêm thất bại!"});

        }
        [HttpPost("UpdateShoppingCart")]
        public async Task<IActionResult> UpdateShoppingCart([FromForm] ShoppingCartDto shoppingCartDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Không hợp lệ.");
            }
            var shoppingCart = ShoppingCartDto.toShoppingCart(shoppingCartDto);
            _unitOfWork.ShoppingCartRepository.UpdateShopingCart(shoppingCart);
            if(await _unitOfWork.Complete())
            {
                return Ok("Cập nhật thành công!");
            }
            return BadRequest("Cập nhật không thành công!");
        }
        [HttpGet("GetShoppingCartById{id}")]
        public async Task<IActionResult> GetShoppingCartByIdAsync(int id)
        {
            var shoppingCart=await _unitOfWork.ShoppingCartRepository.GetShoppingCartByIdAsync(id);
            if(shoppingCart==null) return NotFound("Không tìm thấy thông tin Cart!");
            var shoppingCartDto= ShoppingCart.toShoppingCartGetDto(shoppingCart);
            return Ok(shoppingCartDto);
        }
    }
}