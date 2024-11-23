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

        public static OrderDto FromEntityToDto(Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                SubTotal = order.SubTotal,
                OrderDate = order.Order_date,
                Address = order.Address,
                Phone = order.Phone,
                ShippingFee = order.ShippingFee,
                Created = order.Created,
                Updated = order.Updated,
                ShippingMethodId = order.ShippingMethodId,
                UserId = order.UserId,
                DiscountId = order.DiscountId,
                DiscountPrice = order.DiscountPrice,
                orderItems = order.OrderItems.Select(item => new OrderItemsDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    SizeName=item.SizeName,
                    ProductName=item.ProductName,
                    ColorName=item.ColorName,
                }).ToList()
            };
        }
    }
}