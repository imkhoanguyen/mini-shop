using API.Entities;

namespace API.DTOs
{
    public class ShippingMethodDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public DateTime EstimatedDeliveryTime { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public static ShippingMethod toShippingMethod(ShippingMethodDto shippingMethodDto)
        {
            return new ShippingMethod
            {
                Id=shippingMethodDto.Id,
                Name=shippingMethodDto.Name,
                Cost=shippingMethodDto.Cost,
                EstimatedDeliveryTime=shippingMethodDto.EstimatedDeliveryTime,
                Created=shippingMethodDto.Created,
                Updated=shippingMethodDto.Updated
            };
        }
    }
}