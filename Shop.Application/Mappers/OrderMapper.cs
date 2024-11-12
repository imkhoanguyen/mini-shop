using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Application.DTOs.Orders;
using Shop.Domain.Entities;

namespace Shop.Application.Mappers
{
    public class OrderMapper
    {
        public static Order FromAddDtoToEntity(OrderAddDto dto)
        {
            return new Order
            {
                SubTotal = dto.SubTotal,
                Order_date = dto.OrderDate,
                Address = dto.Address,
                Phone = dto.Phone,
                ShippingFee = dto.ShippingFee,
                UserId = dto.UserId,
                DiscountId = dto.DiscountId,
                DiscountPrice = dto.DiscountPrice,
                ShippingMethodId = dto.ShippingMethodId
            };
        }
    }
}