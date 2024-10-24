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
        private readonly ICloudinaryService _imageService;
        private readonly IVariantRepository _variantRepository; 
        private readonly IVariantRepository _variantRepository;
        private readonly IVoucherRepository _voucherRepository;
        private readonly IImageService _imageService;
        private readonly ICartItemsRepository _cartItemsRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IShippingMethodRepository _shippingMethodRepository;
        private readonly IPaymentsRepository _paymentsRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemsRepository _orderItemsRepository;
        private readonly IUserRepository _userRepository;

        public UnitOfWork(StoreContext context, ICategoryRepository categoryRepository,
             IProductRepository productRepository, ISizeRepository sizeRepository,
             IColorRepository colorRepository, IMessageRepository messageRepository,
             IImageRepository imageRepository, ICloudinaryService imageService,
             IVariantRepository variantRepository,ICartItemsRepository cartItemsRepository,
             IShoppingCartRepository shoppingCartRepository, IReviewRepository reviewRepository)
        {
            _context = context;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _sizeRepository = sizeRepository;
            _colorRepository = colorRepository;
            _messageRepository = messageRepository;
            _imageRepository = imageRepository;
            _imageService = imageService;
            _variantRepository = variantRepository;
            _cartItemsRepository=cartItemsRepository;
            _shoppingCartRepository=shoppingCartRepository;
            _reviewRepository = reviewRepository;
        }
             IImageRepository imageRepository, IVariantRepository variantRepository,
             IVoucherRepository voucherRepository, IImageService imageService,
             ICartItemsRepository cartItemsRepository, IShoppingCartRepository shoppingCartRepository,
            IReviewRepository reviewRepository,IUserRepository userRepository,
             IShippingMethodRepository shippingMethodRepository, IPaymentsRepository paymentsRepository,
             IOrderRepository orderRepository, IOrderItemsRepository orderItemsRepository)
        

            {
                _context = context;
                _categoryRepository = categoryRepository;
                _productRepository = productRepository;
                _sizeRepository = sizeRepository;
                _colorRepository = colorRepository;
                _messageRepository = messageRepository;
                _imageRepository = imageRepository;
                _imageService = imageService;
                _imageService = imageService;
                _variantRepository = variantRepository;
                _voucherRepository = voucherRepository;
                _cartItemsRepository = cartItemsRepository;
                _shoppingCartRepository = shoppingCartRepository;
                _reviewRepository = reviewRepository;
                _shippingMethodRepository = shippingMethodRepository;
                _paymentsRepository = paymentsRepository;
                _orderRepository = orderRepository;
                _orderItemsRepository = orderItemsRepository;
                _reviewRepository = reviewRepository;
                _userRepository = userRepository;
                _voucherRepository = voucherRepository;
                _cartItemsRepository = cartItemsRepository;
                _shoppingCartRepository = shoppingCartRepository;
                _reviewRepository = reviewRepository;
                _shippingMethodRepository = shippingMethodRepository;
                _paymentsRepository = paymentsRepository;
                _orderRepository = orderRepository;
                _orderItemsRepository = orderItemsRepository;
                _reviewRepository = reviewRepository;
                _userRepository = userRepository;
            }
        public ICategoryRepository CategoryRepository => _categoryRepository;
        public IProductRepository ProductRepository => _productRepository;
        public ISizeRepository SizeRepository => _sizeRepository;
        public IColorRepository ColorRepository => _colorRepository;
        public IMessageRepository MessageRepository => _messageRepository;
        public IImageRepository ImageRepository => _imageRepository;
        public ICartItemsRepository CartItemsRepository => _cartItemsRepository;
        public IShoppingCartRepository ShoppingCartRepository => _shoppingCartRepository;

        public ICloudinaryService ImageService => _imageService;
        public IImageService ImageService => _imageService;
        public IReviewRepository ReviewRepository => _reviewRepository;
        public IShippingMethodRepository ShippingMethodRepository => _shippingMethodRepository;
        public IPaymentsRepository PaymentsRepository => _paymentsRepository;
        public IOrderRepository OrderRepository => _orderRepository;
        public IOrderItemsRepository OrderItemsRepository => _orderItemsRepository;
        public IVariantRepository VariantRepository => _variantRepository;
        public IVoucherRepository VoucherRepository => _voucherRepository;
        public IUserRepository UserRepository => _userRepository;

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
