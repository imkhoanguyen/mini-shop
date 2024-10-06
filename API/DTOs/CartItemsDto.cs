using System.ComponentModel.DataAnnotations;
using API.Entities;
using API.Interfaces;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.DTOs
{
    public class CartItemsDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Quantity{get;set;}
        public int ShoppingCartId { get; set; }
        [Required]
        public int VariantId{ get; set; }
        public static CartItems toCartItems(CartItemsDto cartItemsDto)
        {
            return new CartItems
            {
                Id=cartItemsDto.Id,
                Price=cartItemsDto.Price,
                Quantity=cartItemsDto.Quantity,
                ShoppingCartId=cartItemsDto.ShoppingCartId,
                VariantId=cartItemsDto.VariantId
            };
        }
    }
    public class CartItemsGetByShoppingCartIdDto
    {
        [Required]
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Quantity{get;set;}
        public static CartItems toCartItems(CartItemsGetByShoppingCartIdDto cartItemsGetByShoppingCartIdDto)
        {
            return new CartItems
            {
                Id=cartItemsGetByShoppingCartIdDto.Id,
                Price=cartItemsGetByShoppingCartIdDto.Price,
                Quantity=cartItemsGetByShoppingCartIdDto.Quantity
            };
        }
    }
    public class CartItemsAddDto
    {      
        [Required]
        public int VariantId { get; set; }
        public int ShoppingCartId { get; set; }
        public int Quantity { get; set; }
        public static CartItems toCartItems(CartItemsAddDto cartItemsAddDto)
        {
            return new CartItems
            {
                VariantId=cartItemsAddDto.VariantId,
                Quantity=cartItemsAddDto.Quantity,
                ShoppingCartId=cartItemsAddDto.ShoppingCartId
            };
        }
        
    }
    public class CartItemsUpdateDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int VariantId{get;set;}
        public static CartItems toCartItems(CartItemsUpdateDto cartItemsUpdateDto)
        {
            return new CartItems
            {
                Id=cartItemsUpdateDto.Id,
                Quantity=cartItemsUpdateDto.Quantity,
                VariantId=cartItemsUpdateDto.VariantId
            };
        }
    }
    public class CartItemsDeleteDto
    {
        public int Id{get;set;}
        public static CartItems toCartItems(CartItemsDeleteDto cartItemsDeleteDto)
        {
            return new CartItems
            {
                Id=cartItemsDeleteDto.Id
            };
        }
    }
    public class CartItemsGetDto
    {
        public int Id { get; set; }
        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price{get;set;}
    }
}