using API.Entities;
using API.Helpers;
using Shop.Application.Repositories;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;

namespace API.Interfaces
{
    public interface ISizeRepository : IRepository<Size>
    {
        Task<IEnumerable<Size>> GetAllSizesAsync(bool tracked);
        Task<PagedList<Size>> GetAllSizesAsync(SizeParams sizeParams, bool tracked = false);

        Task UpdateAsync(Size size);
        Task DeleteAsync(Size size);
    }
}