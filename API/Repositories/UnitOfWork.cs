﻿using API.Data;
using API.Interfaces;


namespace API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;
        public ICategoryRepository CategoryRepository { get; private set; }

        public IProductRepository ProductRepository { get; private set; }

        public ISizeRepository SizeRepository { get; private set; }
        public IColorRepository ColorRepository { get; private set; }

        public IVariantRepository VariantRepository { get; private set; }

        public ICartItemsRepository CartItemsRepository { get; private set; }

        public IShoppingCartRepository ShoppingCartRepository { get; private set; }

        public IMessageRepository MessageRepository { get; private set; }

        public IReviewRepository ReviewRepository { get; private set; }

        public IShippingMethodRepository ShippingMethodRepository { get; private set; }

        public IPaymentsRepository PaymentsRepository { get; private set; }

        public IOrderRepository OrderRepository { get; private set; }

        public IOrderItemsRepository OrderItemsRepository { get; private set; }

        public IVoucherRepository VoucherRepository { get; private set; }

        public IImageRepository ImageRepository { get; private set; }


        public UnitOfWork(StoreContext context)
        {
            _context = context;
            CategoryRepository = new CategoryRepository(_context);
            ProductRepository = new ProductRepository(_context);
            SizeRepository = new SizeRepository(_context);
            ColorRepository = new ColorRepository(_context);
            VariantRepository = new VariantRepository(_context);
            CartItemsRepository = new CartItemsRepository(_context);
            ShoppingCartRepository = new ShoppingCartRepository(_context);
            MessageRepository = new MessageRepository(_context);
            ReviewRepository = new ReviewRepository(_context);
            ShippingMethodRepository = new ShippingMethodRepository(_context);
            PaymentsRepository = new PaymentsRepository(_context);
            OrderRepository = new OrderRepository(_context);
            VoucherRepository = new VoucherRepository(_context);
            OrderItemsRepository = new OrderItemsRepository(_context);
            ImageRepository = new ImageRepository(_context);
        }

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}