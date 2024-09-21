using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class VariantRepository : IVariantRepository
    {
        private readonly StoreContext _context;
        public VariantRepository(StoreContext context)
        {
            _context = context;
        }
        public void AddVariant(Variant variant)
        {
            var variantDb = _context.Variants.FirstOrDefault(v => v.ProductId == variant.ProductId);
            _context.Variants.Add(variant);
        }

        public void DeleteVariant(Variant variant)
        {
            var variantDb = _context.Variants.FirstOrDefault(v => v.ProductId == variant.ProductId);
            if (variantDb is not null)
            {
                variantDb.IsDelete = true;
            }
        }
        public void UpdateVariant(Variant variant)
        {
            var variantDb = _context.Variants.FirstOrDefault(v => v.ProductId == variant.ProductId);
            if (variantDb is not null)
            {
                variantDb.Price = variant.Price;
                variantDb.Quantity = variant.Quantity;
                variantDb.PriceSell = variant.PriceSell;
                variantDb.Status = variant.Status;
            }
        }

        public async Task<IEnumerable<Variant>> GetAllByProductIdsAsync(IEnumerable<int> productIds)
        {
            return await _context.Variants
                .Where(v => productIds.Contains(v.ProductId) && !v.IsDelete)
                .ToListAsync();
        }

        public async Task<Variant?> GetVariantByProductIdAsync(int productId)
        {
            return await _context.Variants
                .FirstOrDefaultAsync(v => v.ProductId == productId && !v.IsDelete);
        }
    }
}