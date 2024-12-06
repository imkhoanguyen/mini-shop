using API.Interfaces;
using API.Repositories;
using Shop.Application.Repositories;
using Shop.Infrastructure.DataAccess;


namespace Shop.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;
        public IAddressRepository AddressRepository{ get; private set;}
        public ICategoryRepository CategoryRepository { get; private set; }

        public IProductRepository ProductRepository { get; private set; }

        public ISizeRepository SizeRepository { get; private set; }
        public IColorRepository ColorRepository { get; private set; }

        public IVariantRepository VariantRepository { get; private set; }


        public IMessageRepository MessageRepository { get; private set; }

        public IReviewRepository ReviewRepository { get; private set; }

        public IShippingMethodRepository ShippingMethodRepository { get; private set; }


        public IOrderRepository OrderRepository { get; private set; }

        public IOrderItemsRepository OrderItemsRepository { get; private set; }

        public IBlogRepository BlogRepository { get; private set; }
        public IDiscountRepository DiscountRepository{get;private set;}

        public IProductUserLikeRepository ProductUserLikeRepository{get;private set;}

        public UnitOfWork(StoreContext context)
        {
            _context = context;
            AddressRepository = new AddressRepository(_context);
            CategoryRepository = new CategoryRepository(_context);
            ProductRepository = new ProductRepository(_context);
            SizeRepository = new SizeRepository(_context);
            ColorRepository = new ColorRepository(_context);
            VariantRepository = new VariantRepository(_context);
            MessageRepository = new MessageRepository(_context);
            ReviewRepository = new ReviewRepository(_context);
            ShippingMethodRepository = new ShippingMethodRepository(_context);
            OrderRepository = new OrderRepository(_context);
            OrderItemsRepository = new OrderItemsRepository(_context);
            BlogRepository = new BlogRepository(_context);
            DiscountRepository =new DiscountRepository(_context);
            ProductUserLikeRepository=new ProductUserLikeRepository(_context);
        }

        public async Task<bool> CompleteAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}