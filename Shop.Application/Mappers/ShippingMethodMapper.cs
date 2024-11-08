using Shop.Application.DTOs.ShippingMethods;
using Shop.Domain.Entities;

namespace Shop.Application.Mappers
{
    public class ShippingMethodMapper
    {
        public static ShippingMethod ShippingMethodDtoToEntity(ShippingMethodDto shippingMethodDto)
        {
            return new ShippingMethod
            {
                Id = shippingMethodDto.Id,
                Name = shippingMethodDto.Name,
                Cost = shippingMethodDto.Cost,
                EstimatedDeliveryTime = shippingMethodDto.EstimatedDeliveryTime,
                Created = shippingMethodDto.Created,
                Updated = shippingMethodDto.Updated
            };
        }

        public static ShippingMethodDto ShippingMethodEntityToDto(ShippingMethod shippingMethod)
        {
            return new ShippingMethodDto
            {
                Id = shippingMethod.Id,
                Name = shippingMethod.Name,
                Cost = shippingMethod.Cost,
                EstimatedDeliveryTime = shippingMethod.EstimatedDeliveryTime,
                Created = shippingMethod.Created,
                Updated = shippingMethod.Updated
            };
        }
    }
}
