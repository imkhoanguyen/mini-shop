namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        ISizeRepository SizeRepository { get; }
        IColorRepository ColorRepository{ get; }
        IVariantRepository VariantRepository { get; }
        IImageRepository ImageRepository { get; }
        IMessageRepository MessageRepository { get; }
        
        IBlogRepository BlogRepository { get; }
        

        Task<bool> Complete();
    }
}