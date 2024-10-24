using API.Data;
using API.Interfaces;

namespace API.Repositories
{
    public class OrderItemsRepository : IOrderItemsRepository
    {
        private readonly StoreContext _context;
        public OrderItemsRepository(StoreContext context)
        {
            _context = context;
        }
    }
}