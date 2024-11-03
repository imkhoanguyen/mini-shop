using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Helpers;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;

namespace Shop.Application.Services.Abstracts
{
    public interface ISizeService
    {
        Task<IEnumerable<Size>> GetAllSizesAsync(bool tracked);
        Task<PagedList<Size>> GetAllSizesAsync(SizeParams sizeParams, bool tracked = false);

        Task<Size?> GetSizesById(int id);
        Task DeleteAsync(Size size);
        Task UpdateAsync(Size size);
        Task<IEnumerable<Size>> GetAllSizesAsync();
        Task<bool> SizeExistsAsync(string name);

        Task AddSize(Size size);

        Task<bool> CompleteAsync();
    }
}