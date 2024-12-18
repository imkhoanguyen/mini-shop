
using Shop.Application.Repositories;
using Shop.Domain.Entities;

namespace API.Interfaces
{
    public interface IVariantRepository : IRepository<Variant>
    {
        Task UpdateVariantAsync(Variant variant);
        Task DeleteVariantAsync(Variant variant);
        Task<IEnumerable<Variant>> GetByProductIdAsync(int productId);

    }
}