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
        private readonly IMessageRepository _messageRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IVariantRepository _variantRepository;
        private readonly IVoucherRepository _voucherRepository;

        public UnitOfWork(StoreContext context, ICategoryRepository categoryRepository,
             IProductRepository productRepository, ISizeRepository sizeRepository,
             IColorRepository colorRepository, IMessageRepository messageRepository,
             IImageRepository imageRepository, IVariantRepository variantRepository,
             IVoucherRepository voucherRepository)
        {
            _context = context;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _sizeRepository = sizeRepository;
            _colorRepository = colorRepository;
            _messageRepository = messageRepository;
            _imageRepository = imageRepository;
            _variantRepository = variantRepository;
            _voucherRepository=voucherRepository;
        }
        public ICategoryRepository CategoryRepository => _categoryRepository;
        public IProductRepository ProductRepository => _productRepository;
        public ISizeRepository SizeRepository => _sizeRepository;
        public IColorRepository ColorRepository => _colorRepository;
        public IMessageRepository MessageRepository => _messageRepository;
        public IImageRepository ImageRepository => _imageRepository;
        public IVariantRepository VariantRepository => _variantRepository;

        public IVoucherRepository VoucherRepository => _voucherRepository;

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
