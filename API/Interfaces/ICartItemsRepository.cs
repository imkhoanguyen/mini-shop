using API.Entities;
 namespace API.Interfaces
 {
    public interface ICartItemsRepository
    {
        void AddCartItems(CartItems cartItems);
        void UpdateCartItems(CartItems cartItems);
        void DeleteCartItems(CartItems cartItems);
        //Task<CartItems?> GetCartItemsByShoppingCartIdAsync(int ShoppingCartId);
        
    }
 }