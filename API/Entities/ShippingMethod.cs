using System.ComponentModel.DataAnnotations.Schema;
using API.Data.Enum;
using API.DTOs;

namespace API.Entities
{
    public class ShippingMethod : BaseEntity
    {
       
        public string? name { get; set; }
        public decimal? cost { get; set; }
        public DateTime? estimatedDeliveryTime { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        
        public static ShippingMethodDto toShippingMethodDto(ShippingMethod ShippingMethod)
        {
            return new ShippingMethodDto
            {
                name=ShippingMethod.name,
                cost=ShippingMethod.cost,
                estimatedDeliveryTime=ShippingMethod.estimatedDeliveryTime,
                created_at=ShippingMethod.created_at,
                updated_at=ShippingMethod.updated_at
            };
        }
    }
}