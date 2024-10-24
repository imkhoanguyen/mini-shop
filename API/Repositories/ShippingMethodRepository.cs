using API.Data;
using API.Entities;
using API.Interfaces;

namespace API.Repositories
{
    public class ShippingMethodRepository : IShippingMethodRepository
    {
        private readonly StoreContext _context;
        public ShippingMethodRepository(StoreContext context)
        {
            _context=context;
        }
        public void AddShippingMethod(ShippingMethod shippingMethod)
        {
            _context.ShippingMethods.Add(shippingMethod);
        }

        public void DeleteShippingMethod(ShippingMethod shippingMethod)
        {
            var shippingMethodDb = _context.ShippingMethods.FirstOrDefault(sm=>sm.Id==shippingMethod.Id);
            if(shippingMethodDb is not null)
            {
                _context.ShippingMethods.Remove(shippingMethod);
            }
        }

        public void UpdateShippingMethod(ShippingMethod shippingMethod)
        {
            var shippingMethodDb=_context.ShippingMethods.FirstOrDefault(sm=>sm.Id==shippingMethod.Id);
            if(shippingMethodDb is not null)
            {
                
            }
            
        }
    }
}