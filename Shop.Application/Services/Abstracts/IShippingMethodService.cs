using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Domain.Entities;

namespace Shop.Application.Services.Abstracts
{
    public interface IShippingMethodService
    {
        Task<IEnumerable<ShippingMethod>> GetShippingMethodsAsync(bool tracked);
    }
}