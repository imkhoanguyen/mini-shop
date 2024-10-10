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
        IMessageRepository MessageRepository { get; }
        IShippingMethodRepository ShippingMethodRepository{get;}
        IPaymentsRepository PaymentsRepository{get;}
        IOrderRepository OrderRepository{get;}
        IOrderItemsRepository OrderItemsRepository{get;}
        

        Task<bool> Complete();
    }
}