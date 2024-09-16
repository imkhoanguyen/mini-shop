using API.Data;
using API.Interfaces;

namespace API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;
        private readonly ICategoryRepository _categoryRepository;
      
        private readonly IProductRepository _productRepository;
        
        public UnitOfWork(StoreContext context, ICategoryRepository categoryRepository,
             IProductRepository productRepository)
        {
            _context = context;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }
        public ICategoryRepository CategoryRepository => _categoryRepository;
      
        public IProductRepository ProductRepository => _productRepository;

		public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
