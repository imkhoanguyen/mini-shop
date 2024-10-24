using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Repositories;
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
        public async Task<IActionResult> AddCartItems([FromBody]CartItemsAddDto cartItemsAddDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new {message="Dữ liệu không hợp lệ!"});
            }
            //if (variantsDb == null) throw new Exception("Không tìm thấy Item");
            var cartItems =CartItemsAddDto.toCartItems(cartItemsAddDto);
            var variantsDb=await _unitOfWork.CartItemsRepository.GetVariantById(cartItems.VariantId);
            var cartexist= await _unitOfWork.CartItemsRepository.CheckExist(cartItems);
            if(cartexist is not null )
            {
                return BadRequest(new{message="Sản phẩm đã có sẵn trong giỏ hàng!"});
            }
            if(cartItems.Quantity >variantsDb.Quantity)
            {
                return BadRequest( new{message="Số lượng hiện thời không đủ!"});
            }

            cartItems.Price=variantsDb.Price;
            _unitOfWork.CartItemsRepository.AddCartItems(cartItems);
            if (await _unitOfWork.Complete())
            {
                return Ok(new{message="Thêm CartItems thành công!"});
                
            }
            return BadRequest(new {message="Thêm CartItems không thành công!"});
        }
        [HttpPut("UpdateCartItems")]
        public async Task<IActionResult> UpdateCartItems([FromBody]CartItemsUpdateDto cartItemsUpdateDto)
        {
                if(!ModelState.IsValid)
                {
                    return BadRequest(new {message="Dữ liệu không hợp lệ!"});
                }
                
                var cartItems=CartItemsUpdateDto.toCartItems(cartItemsUpdateDto);
                var variantsDb = await _unitOfWork.CartItemsRepository.GetVariantById(cartItems.VariantId);
                if (cartItems.Quantity>variantsDb.Quantity)
                {
                    return BadRequest(new {message="Số lương hiện thời không đủ!"});
                }
                else if(cartItems.Quantity==0)
                {
                    _unitOfWork.CartItemsRepository.DeleteCartItems(cartItems);
                    return Ok(new{message="Bạn đã xóa Items {variantsDb.Color} thành công!"});
                }
                _unitOfWork.CartItemsRepository.UpdateCartItems(cartItems);
                if(await _unitOfWork.Complete())
                {
                    return Ok(new {message="Thay đổi thành công"});
                }
                return BadRequest(new {message="Thay đổi thất bại!"});
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteCartItems(CartItemsDeleteDto cartItemsDeleteDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var cartItem=CartItemsDeleteDto.toCartItems(cartItemsDeleteDto);
            _unitOfWork.CartItemsRepository.DeleteCartItems(cartItem);
            if(await _unitOfWork.Complete())
            {
                return Ok("Xoa thanh cong");
            }
            return BadRequest("xoa that bai");
        }
        [HttpGet("GetBySHoppingCartId{id}")]
        public async Task<IActionResult> GetCartItemsByShoppingCartIdAsync(int id)
        {
            var cartItem =await _unitOfWork.CartItemsRepository.GetCartItemsByShoppingCartIdAsync(id);
            if (cartItem==null)return NotFound("Chưa có hàng trong giỏ!");
            var cartItemsDto=cartItem.Select(ci=>CartItems.toCartItem(ci)).ToList();
            return Ok(cartItem);

        }
        [HttpGet]
        public async Task<IActionResult> GetCartItemById(int id)
        {
            var cartItemDb =await _unitOfWork.CartItemsRepository.GetCartItemsById(id);
            if(cartItemDb==null)return NotFound(new{message="Không tìm thấy Items!"});
            var cartItem=CartItems.toCartItem(cartItemDb);
            return Ok(cartItem);
        }
    }
}