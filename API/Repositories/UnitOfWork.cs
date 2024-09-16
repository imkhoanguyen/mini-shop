

using api.Interfaces;
using API.Data;
using API.Interfaces;

namespace API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;
        private readonly ICategoryRepository _categoryRepository;
      
        private readonly IProductRepository _productRepository;
        
        private readonly ISizeRepository _sizeRepository;
        private readonly IColorRepository _colorRepository;
        public UnitOfWork(StoreContext context, ICategoryRepository categoryRepository,
             IProductRepository productRepository, ISizeRepository sizeRepository)
        {
            _context = context;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _sizeRepository = sizeRepository;
        }
        public ICategoryRepository CategoryRepository => _categoryRepository;
      
        public IProductRepository ProductRepository => _productRepository;

        public ISizeRepository SizeRepository => _sizeRepository;

        public IColorRepository colorRepository => _colorRepository;

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
