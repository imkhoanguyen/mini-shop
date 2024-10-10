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
        IVoucherRepository VoucherRepository { get; }

        Task<bool> Complete();
    }
}