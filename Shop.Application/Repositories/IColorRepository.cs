using API.Helpers;
using Shop.Application.Repositories;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;

namespace API.Interfaces
{
    public interface IColorRepository : IRepository<Color>  
    {
        Task<PagedList<Color>> GetAllColorsAsync(ColorParams colorParams, bool tracked);
        Task<IEnumerable<Color>> GetAllColorsAsync(bool tracked = false);
        Task DeleteAsync(Color color);
        Task UpdateAsync(Color color);
    }
}