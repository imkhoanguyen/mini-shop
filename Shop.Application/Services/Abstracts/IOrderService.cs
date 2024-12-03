using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Domain.Entities;
using Shop.Application.DTOs.Orders;
using System.Linq.Expressions;
using Shop.Application.Ultilities;
using Shop.Application.Parameters;


namespace Shop.Application.Services.Abstracts
{
    public interface IOrderService
    {
        Task<OrderDto> AddAsync(OrderAddDto dto);
        Task<OrderDto> GetAsync(Expression<Func<Order, bool>> expression, bool tracked = false);
        Task<PagedList<OrderDto>> GetAllAsync(OrderParams prm, bool tracked = false);
        Task<OrderDto> UpdateStatus(int id, string status);
        Task<OrderDto> UpdatePaymentStatus(int id, string status);
        Task DeleteOrderAsync(Expression<Func<Order, bool>> expression);
        Task<bool> CheckOrderItems(OrderAddDto order);
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