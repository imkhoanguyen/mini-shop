using API.Interfaces;

namespace Shop.Application.Repositories
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        ISizeRepository SizeRepository { get; }
        IColorRepository ColorRepository { get; }
        IVariantRepository VariantRepository { get; }
        IImageRepository ImageRepository { get; }
        IMessageRepository MessageRepository { get; }
        IReviewRepository ReviewRepository { get; }
        IShippingMethodRepository ShippingMethodRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderItemsRepository OrderItemsRepository { get; }
        IVoucherRepository VoucherRepository { get; }
        IBlogRepository BlogRepository { get; }
        IDiscountRepository DiscountRepository{get;}
        Task<bool> CompleteAsync();
    }
}