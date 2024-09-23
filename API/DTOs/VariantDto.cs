using System.ComponentModel.DataAnnotations;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.DTOs
{
    public class VariantDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal PriceSell { get; set; }
        public int Quantity { get; set; }
        [Required]
        public int SizeId { get; set; }

        [Required]
        public int ColorId { get; set; }
        public static Variant toVariant(VariantDto variantDto)
        {
            return new Variant
            {
                Id = variantDto.Id,
                Price = variantDto.Price,
                PriceSell = variantDto.PriceSell,
                Quantity = variantDto.Quantity,
                SizeId = variantDto.SizeId,
                ColorId = variantDto.ColorId
            };
        }
    }
    public class VariantAddDto
    {
        [Required]
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public decimal PriceSell { get; set; }
        public int Quantity { get; set; }
        
        [Required]
        public int SizeId { get; set; }

        [Required]
        public int ColorId { get; set; }
        public static Variant toVariant(VariantAddDto variantAddDto)
        {
            return new Variant
            {
                ProductId = variantAddDto.ProductId,
                Price = variantAddDto.Price,
                PriceSell = variantAddDto.PriceSell,
                Quantity = variantAddDto.Quantity,
                SizeId = variantAddDto.SizeId,
                ColorId = variantAddDto.ColorId
            };
        }
    }
}