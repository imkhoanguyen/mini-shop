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
        Task<OrderDto> AddAsync(OrderAddDto dto);
        Task<List<OrderDto>> GetOrdersByUserIdAsync(string userId);
        Task<decimal> GetTotalRevenueByDateAsync(DateTime date);
        Task<decimal> GetTotalRevenueByMonthAsync(int year, int month);
        Task<decimal> GetTotalRevenueByYearAsync(int year);
        Task<int> CountOrdersTodayAsync();
        Task<int> CountOrdersByDateAsync(DateTime date);
        Task<int> CountOrdersByMonthAsync(int year, int month);
        Task<int> CountOrdersByYearAsync(int year);
    }
}