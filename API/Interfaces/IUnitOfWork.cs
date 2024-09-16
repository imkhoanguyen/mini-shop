namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        ISizeRepository SizeRepository { get; }
        IColorRepository colorRepository{ get; }

        Task<bool> Complete();
    }
}