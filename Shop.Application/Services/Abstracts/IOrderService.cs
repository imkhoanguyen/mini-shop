using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Domain.Entities;
using Shop.Application.DTOs.Orders;


namespace Shop.Application.Services.Abstracts
{
    public interface IOrderService
    {
        Task AddAsync(Order order);
        Task<List<OrderDto>> GetOrdersByUserIdAsync(string userId);
    }
}