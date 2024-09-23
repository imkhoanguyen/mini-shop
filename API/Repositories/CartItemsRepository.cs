using API.Data;
using API.Entities;
using API.Interfaces;
using CloudinaryDotNet.Actions;

namespace API.Repositories
{
    public class CartItemsRepository : ICartItemsRepository
    {
        private readonly StoreContext _context;
        public CartItemsRepository(StoreContext context)
        {
            _context =context;
        }
        public void AddCartItems(CartItems cartItems)
        {
            _context.CartItems.Add(cartItems);
        }
        public void UpdateCartItems(CartItems cartItems)
        {
            var cartItemDb=_context.CartItems.FirstOrDefault(ci=>ci.Id==cartItems.Id);
            if (cartItemDb is not null)
            {
                cartItemDb.Price=cartItems.Price;
                cartItemDb.Quantity=cartItems.Quantity;
            }
        }
        public void DeleteCartItems(CartItems cartItems)
        {
            var cartItemDb=_context.CartItems.FirstOrDefault(ci=>ci.Id==cartItems.Id);
            
        }
        
    }
}