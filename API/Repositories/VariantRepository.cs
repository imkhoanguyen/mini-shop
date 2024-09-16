using API.Data;
using API.Entities;
using API.Interfaces;

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
            _context.Variants.Add(variant);
            _context.SaveChanges();
        }

        public void DeleteVariant(Variant variant)
        {
            var variantDb = _context.Variants.FirstOrDefault(v => v.Id == variant.Id);
            if(variantDb is not null)
            {
                variantDb.IsDelete = true;
                _context.SaveChanges();
            }
        }

        public void UpdateVariant(Variant variant)
        {
            var variantDb = _context.Variants.FirstOrDefault(v => v.Id == variant.Id);
            if(variantDb is not null)
            {
                variantDb.Price = variant.Price;
                variantDb.Quantity = variant.Quantity;
                variantDb.PriceSell = variant.PriceSell;
                variantDb.Status = variant.Status;
                _context.SaveChanges();
            }
        }
    }
}