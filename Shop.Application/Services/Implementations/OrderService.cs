using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Application.Repositories;
using Shop.Application.Services.Abstracts;
using Shop.Domain.Entities;
using Shop.Application.DTOs.Orders;
using Shop.Application.Mappers;
using Shop.Domain.Exceptions;
using System.Linq.Expressions;

namespace Shop.Application.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderDto> AddAsync(OrderAddDto dto)
        {
           var order = OrderMapper.FromAddDtoToEntity(dto);

            order.OrderItems = dto.Items.Select(item => new OrderItems
            {
                Quantity = item.Quantity,
                Price = item.Price,
                ProductName = item.ProductName,
                ProductId = item.ProductId,
                SizeName = item.SizeName,
                ColorName = item.ColorName,
                ProductImage = item.ProductImage,
            }).ToList();

            await _unitOfWork.OrderRepository.AddAsync(order);

            if (await _unitOfWork.CompleteAsync())
            {
                return OrderMapper.FromEntityToDto(order);
            }

            throw new BadRequestException("Có lỗi xảy ra khi thêm order");
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
        public async Task<int> CountOrdersByDateAsync(DateTime date)
        {
            return await _unitOfWork.OrderRepository.CountOrdersByDateAsync(date);
        }
        public async Task<int> CountOrdersByMonthAsync(int year, int month)
        {
            return await _unitOfWork.OrderRepository.CountOrdersByMonthAsync(year, month);
        }
        public async Task<int> CountOrdersByYearAsync(int year)
        {
            return await _unitOfWork.OrderRepository.CountOrdersByYearAsync(year);
        }

        public async Task<OrderDto> GetAsync(Expression<Func<Order, bool>> expression, bool tracked = false)
        {
            var order =  await _unitOfWork.OrderRepository.GetAsync(expression, tracked);

            if (order == null)
                throw new NotFoundException("Không tìm thấy đơn hàng");

            return OrderMapper.FromEntityToDto(order);
        }
    }
}