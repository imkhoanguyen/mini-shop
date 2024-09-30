using System.ComponentModel.DataAnnotations;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.DTOs
{
    public class CartItemsDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Quantity{get;set;}
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int ShoppingCartId { get; set; }
        public static CartItems toCartItems(CartItemsDto cartItemsDto)
        {
            return new CartItems
            {
                Id=cartItemsDto.Id,
                Price=cartItemsDto.Price,
                Quantity=cartItemsDto.Quantity,
                ProductId=cartItemsDto.ProductId,
                ShoppingCartId=cartItemsDto.ShoppingCartId
            };
        }
    }
    public class CartItemsAddDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int ShoppingCartId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public static CartItems toCartItems(CartItemsAddDto cartItemsAddDto)
        {
            return new CartItems
            {
                Price=cartItemsAddDto.Price,
                Quantity=cartItemsAddDto.Quantity,
                ProductId=cartItemsAddDto.ProductId,
                ShoppingCartId=cartItemsAddDto.ShoppingCartId
            };
        }
        
    }
    public class CartItemsUpdateDto
    {
        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public static CartItems toCartItems(CartItemsUpdateDto cartItemsUpdateDto)
        {
            return new CartItems
            {
                Price=cartItemsUpdateDto.Price,
                Quantity=cartItemsUpdateDto.Quantity,
                ProductId=cartItemsUpdateDto.ProductId,
                ShoppingCartId=cartItemsUpdateDto.ShoppingCartId
            };
        }
    }
}