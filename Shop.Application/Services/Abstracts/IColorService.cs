using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Helpers;
using Shop.Application.Services.Abstracts;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;

namespace Shop.Application.Services.Abstracts
{
    public interface IColorService
    {
        Task<IEnumerable<Color>> GetAllColorsAsync(bool tracked);
        Task<PagedList<Color>> GetAllColorsAsync(ColorParams ColorParams, bool tracked = false);

        Task<Color?> GetColorsById(int id);
        Task DeleteAsync(Color Color);
        Task UpdateAsync(Color Color);
        Task<IEnumerable<Color>> GetAllColorsAsync();
        Task<bool> ColorExistsAsync(string name);

        Task AddColor(Color Color);

        Task<bool> CompleteAsync();
    }
}