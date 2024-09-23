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
        IShoppingCartRepository ShoppingCartRepository{get;}
        IImageRepository ImageRepository { get; }
        IMessageRepository MessageRepository { get; }

        Task<bool> Complete();
    }
}