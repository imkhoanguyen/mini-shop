using CloudinaryDotNet.Actions;

namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        ISizeRepository SizeRepository { get; }
        IColorRepository ColorRepository{ get; }
        IVariantRepository VariantRepository { get; }
        ICartItemsRepository CartItemsRepository {get;}
        IShoppingCartRepository ShoppingCartRepository{get;}
        IImageRepository ImageRepository { get; }
        IImageService ImageService { get; }
        IMessageRepository MessageRepository { get; }
        IReviewRepository ReviewRepository { get; }
        IShippingMethodRepository ShippingMethodRepository{get;}
        IPaymentsRepository PaymentsRepository{get;}
        IOrderRepository OrderRepository{get;}
        IOrderItemsRepository OrderItemsRepository{get;}
        
        IReviewRepository ReviewRepository { get; }
        IUserRepository UserRepository { get; }
        IVoucherRepository VoucherRepository { get; }
        IVoucherRepository VoucherRepository { get; }

        Task<bool> Complete();
    }
}