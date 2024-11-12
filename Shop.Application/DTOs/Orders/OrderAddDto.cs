using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.Orders
{
    public class OrderAddDto
    {
        public decimal SubTotal { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public decimal ShippingFee { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public required string UserId { get; set; }
        public int DiscountId { get; set; }
        public int ShippingMethodId { get; set; }
        public decimal DiscountPrice { get; set; }
    }
}