using API.Entities;
 namespace API.Interfaces
 {
    public interface ICartItemsRepository
    {
        void AddCartItems(CartItems cartItems);
        void UpdateCartItems(CartItems cartItems);
        void DeleteCartItems(CartItems cartItems);
        Task<CartItems?>CheckExist(CartItems cartItems);
        //Task<CartItems?> GetCartItemsByShoppingCartIdAsync(int ShoppingCartId);
        Task<IEnumerable<CartItems?>> GetCartItemsByShoppingCartIdAsync(int id);
        Task<Variant?> GetVariantById(int id);
        Task<CartItems?>GetCartItemsById(int id);
    }
 }