using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Application.Services.Implementations;
using Shop.Domain.Entities;

namespace Shop.Application.Services.Abstracts
{
    public interface IOrderItemsService
    {
        Task AddAsync(OrderItems orderItems);

    }
}