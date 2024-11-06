using API.Interfaces;
using Shop.Domain.Entities;
using Shop.Infrastructure.DataAccess;

namespace Shop.Infrastructure.Repositories
{
    public class OrderItemsRepository : Repository<OrderItems>, IOrderItemsRepository
    {
        private readonly StoreContext _context;
        public OrderItemsRepository(StoreContext context) : base(context)
        {
            _context = context;
        }

    }
}