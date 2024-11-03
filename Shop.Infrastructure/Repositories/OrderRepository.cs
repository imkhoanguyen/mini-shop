using Shop.Application.Repositories;
using Shop.Domain.Entities;
using Shop.Infrastructure.DataAccess;

namespace Shop.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly StoreContext _context;
        public OrderRepository(StoreContext context) : base(context)
        {
            _context = context;
        }
    }
}