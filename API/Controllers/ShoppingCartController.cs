using API.DTOs;
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

        [HttpPost("AddShoppongCart")]
        public void AddShoppongCart([FromForm] ShoppingCartAddDto shoppingCartAddDto)
        {
            var shoppingCart=ShoppingCartAddDto.toShoppingCart(shoppingCartAddDto);
            _unitOfWork.ShoppingCartRepository.AddShoppingCart(shoppingCart);
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

    }
}