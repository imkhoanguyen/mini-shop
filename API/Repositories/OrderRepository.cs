using API.Data;
using API.Interfaces;

namespace API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreContext _context;
        public OrderRepository(StoreContext context)
        {
            _context = context;
        }
    }
}