using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.OrderItems
{
    public class OrderItemsAddDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public required string ProductName { get; set; }
        public required string SizeName { get; set; }
        public required string ColorName { get; set; }
        public required string ProductImage { get; set; }

        public int OrderId { get; set; }

    }
}