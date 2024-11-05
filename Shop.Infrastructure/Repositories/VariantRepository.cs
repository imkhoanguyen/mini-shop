using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Infrastructure.DataAccess;

namespace Shop.Infrastructure.Repositories
{
    public class VariantRepository : Repository<Variant>, IVariantRepository
    {
        private readonly StoreContext _context;
        public VariantRepository(StoreContext context) : base(context)
        {
            _context = context;
        }

        public async Task DeleteVariantAsync(Variant variant)
        {
            var variantDb = await _context.Variants.FirstOrDefaultAsync(v => v.Id == variant.Id);
            if (variantDb is not null)
            {
                variantDb.IsDelete = true;
            }
        }

        public async Task<IEnumerable<Variant>> GetByProductIdAsync(int productId)
        {
            return await _context.Variants
                .Include(v => v.Images)
                .Where(v => v.ProductId == productId && !v.IsDelete)
                .ToListAsync();
        }

        public async Task UpdateVariantAsync(Variant variant)
        {
            var variantDb = await _context.Variants.FirstOrDefaultAsync(v => v.Id == variant.Id);
            if (variantDb is not null)
            {
                variantDb.Price = variant.Price;
                variantDb.Quantity = variant.Quantity;
                variantDb.PriceSell = variant.PriceSell;
                variantDb.Status = variant.Status;
            }
        }

    }
}