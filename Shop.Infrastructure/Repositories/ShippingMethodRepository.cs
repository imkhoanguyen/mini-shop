using System.Linq.Expressions;
using Shop.Application.Parameters;
using Shop.Application.Repositories;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Infrastructure.DataAccess;
using Shop.Application.Ultilities;
using Shop.Infrastructure.Ultilities;

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
                shippingMethodDb.IsDelete = true;
            }
        }
        public override async Task<ShippingMethod?>GetAsync(Expression<Func<ShippingMethod,bool>>expression, bool tracked=false)
        {
            var query = _context.ShippingMethods.Where(sm=>!sm.IsDelete).AsQueryable();
            if(!tracked)
                query= query.AsNoTracking();
            return await query.FirstOrDefaultAsync(expression);
        }
        public async Task<IEnumerable<ShippingMethod>> GetAllShippingMethodsAsync(bool tracked = false)
        {
            if (tracked)
                return await _context.ShippingMethods.Where(c => !c.IsDelete).ToListAsync();
            return await _context.ShippingMethods.AsNoTracking().Where(c => !c.IsDelete).ToListAsync();
        }

        public async Task UpdateShippingMethodAsync(ShippingMethod shippingMethod)
        {
            var shippingMethodDb =await _context.ShippingMethods.FirstOrDefaultAsync(sm=>sm.Id==shippingMethod.Id);
            if(shippingMethodDb is not null )
            {
                shippingMethodDb.Name=shippingMethod.Name;
                shippingMethodDb.Cost=shippingMethod.Cost;
                shippingMethodDb.EstimatedDeliveryTime=shippingMethod.EstimatedDeliveryTime;
                shippingMethodDb.Updated=DateTime.UtcNow;
            }
        }

        public async Task<PagedList<ShippingMethod>> GetAllShippingMethodAsync(ShippingMethodParameters shippingMethodParameters, bool tracked = false)
        {
            var query=tracked ? _context.ShippingMethods.AsQueryable().Where(sm=>!sm.IsDelete)
                : _context.ShippingMethods.AsNoTracking().AsQueryable().Where(sm=>!sm.IsDelete);
            if(!string.IsNullOrEmpty(shippingMethodParameters.Search))
            {
                query=query.Where(sm=>sm.Name.ToLower().Contains(shippingMethodParameters.Search.ToLower())
                    || sm.Id.ToString()== shippingMethodParameters.Search);
            }
            if (!string.IsNullOrEmpty(shippingMethodParameters.OrderBy))
            {
                query = shippingMethodParameters.OrderBy switch
                {
                    "id" => query.OrderBy(sm => sm.Id),
                    "id_desc" => query.OrderByDescending(sm => sm.Id),
                    "name" => query.OrderBy(sm => sm.Name.ToLower()),
                    "name_desc" => query.OrderByDescending(sm => sm.Name.ToLower()),
                    _ => query.OrderByDescending(sm => sm.Id)
                };
            }

            return await query.ApplyPaginationAsync(shippingMethodParameters.PageNumber,shippingMethodParameters.PageSize);
        
        }
    }
}