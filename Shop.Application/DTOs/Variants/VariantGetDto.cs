﻿using API.DTOs;

namespace Shop.Application.DTOs.Variants
{
    public class VariantGetDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal PriceSell { get; set; }
        public int Quantity { get; set; }
        public int? SizeId { get; set; }
        public int? ColorId { get; set; }
        //public List<ImageGetDto> ImageUrls { get; set; } = new List<ImageGetDto>();
    }
}
