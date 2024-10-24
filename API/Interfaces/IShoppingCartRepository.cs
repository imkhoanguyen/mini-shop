using API.Entities;
namespace API.Interfaces
{
    public interface IShoppingCartRepository{
        void AddShoppingCart(ShoppingCart shoppingCart);
        void UpdateShopingCart(ShoppingCart shoppingCart);
        void DeleteShoppingCart(ShoppingCart shoppingCart);
        
        Task<ShoppingCart?> GetShoppingCartByIdAsync(int id);
    }
}