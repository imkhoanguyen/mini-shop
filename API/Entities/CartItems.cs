using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.DTOs;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace API.Entities
{
    public class CartItems : BaseEntity
    {
        [Required]
        public int ShoppingCartId{get;set;}
        public int VariantId{get;set;}
        [ForeignKey("VariantId")]
        public Variant? Variant{get;set;}
        [ForeignKey("ShoppingCartId")]
        public ShoppingCart? ShoppingCart{get;set;}
        public int Quantity{get;set;}
        public decimal Price{get;set;}
        public decimal GetTotal()
        {
            return Quantity*Price;
        }
        public static CartItemsDto toCartItem(CartItems cartItems)
        { 
            return new CartItemsDto
            {
                Id=cartItems.Id,
                VariantId=cartItems.VariantId,
                Price=cartItems.Price,
                Quantity=cartItems.Quantity
            };
        }


    }
}