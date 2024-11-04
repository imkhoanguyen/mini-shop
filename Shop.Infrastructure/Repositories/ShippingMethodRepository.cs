using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Infrastructure.DataAccess;

namespace Shop.Infrastructure.Repositories
{
    public class ShippingMethodRepository : Repository<ShippingMethod>, IShippingMethodRepository
    {
        private readonly StoreContext _context;
        public ShippingMethodRepository(StoreContext context) : base(context)
        {
            _context = context;
        }

        public async Task DeleteShippingMethodAsync(ShippingMethod shippingMethod)
        {
            var shippingMethodDb = await _context.ShippingMethods.FirstOrDefaultAsync(sm => sm.Id == shippingMethod.Id);
            if (shippingMethodDb is not null)
            {
                shippingMethod.IsDelete = true;
            }
        }

        public Task UpdateShippingMethodAsync(ShippingMethod shippingMethod)
        {
            throw new NotImplementedException();
        }
        
    }
}