using System.ComponentModel.DataAnnotations;
using API.DTOs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;
namespace API.Entities
{
    public class ShoppingCart : BaseEntity
    {
        
        public DateTime Created{get;set;}=DateTime.UtcNow;
        public DateTime? Updated{get;set;}
        public List<CartItems>?CartItems{get;set;}
        public static ShoppingCartGetDto toShoppingCartGetDto(ShoppingCart shoppingCart)
        {
            return new ShoppingCartGetDto
            {
                Id=shoppingCart.Id,
                CartItems = shoppingCart.CartItems.Select(ci => new CartItemsGetByShoppingCartIdDto
                {
                    Id = ci.Id,
                    Price = ci.Price,
                    Quantity = ci.Quantity
                }).ToList()
            };
        }
    }

}