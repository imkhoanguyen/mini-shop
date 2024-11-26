using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Application.Repositories;
using Shop.Application.Services.Abstracts;
using Shop.Domain.Entities;
using Shop.Application.DTOs.Orders;
using Shop.Application.Mappers;

namespace Shop.Application.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), "order not null");
            }

            await _unitOfWork.OrderRepository.AddAsync(order);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<OrderDto>> GetOrdersByUserIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("User ID must not be null or empty.", nameof(userId));
            }

            // Lấy danh sách Order từ repository
            var orders = await _unitOfWork.OrderRepository.GetOrdersByUserIdAsync(userId);

            // Ánh xạ từ Order sang OrderDto
            return orders.Select(OrderMapper.FromEntityToDto).ToList();
        }

        public async Task<decimal> GetTotalRevenueByDateAsync(DateTime date)
        {
            return await _unitOfWork.OrderRepository.GetTotalRevenueByDateAsync(date);
        }

        public async Task<decimal> GetTotalRevenueByMonthAsync(int year, int month)
        {
            return await _unitOfWork.OrderRepository.GetTotalRevenueByMonthAsync(year, month);
        }
        public async Task<decimal> GetTotalRevenueByYearAsync(int year)
        {
            return await _unitOfWork.OrderRepository.GetTotalRevenueByYearAsync(year);
        }
        public async Task<int> CountOrdersTodayAsync()
        {
            return await _unitOfWork.OrderRepository.CountOrdersTodayAsync();
        }
        public async Task<int> CountOrdersByMonthAsync(int year, int month)
        {
            return await _unitOfWork.OrderRepository.CountOrdersByMonthAsync(year, month);
        }
        public async Task<int> CountOrdersByYearAsync(int year)
        {
            return await _unitOfWork.OrderRepository.CountOrdersByYearAsync(year);
        }


    }
}