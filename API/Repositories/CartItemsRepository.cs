using API.Data;
using API.Entities;
using API.Interfaces;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class CartItemsRepository : ICartItemsRepository
    {
        private readonly StoreContext _context;
        private readonly VariantRepository _variantRepository;
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
            var variantsDb= _context.Variants.FirstOrDefault(v=>v.Id==cartItems.VariantId);
            if(variantsDb is not null)
            {
                var cartItemDb=_context.CartItems.FirstOrDefault(ci=>ci.Id==cartItems.Id );
                if (cartItemDb is not null && cartItems.Quantity<=variantsDb.Quantity && cartItems.Quantity>=0)
                {
                        cartItemDb.Quantity=cartItems.Quantity;
                }
            }
            
        }
        public void DeleteCartItems(CartItems cartItems)
        {
            var cartItemDb=_context.CartItems.FirstOrDefault(ci=>ci.Id==cartItems.Id || ci.Quantity==0);
            if(cartItemDb is not null)
            {
                _context.CartItems.Remove(cartItemDb);
            }
        }
        public async Task<Variant?> GetVariantById(int id)
        {
            return await _context.Variants.FindAsync(id);
        }

        public async Task<CartItems?> CheckExist(CartItems cartItems)
        {
            var cartItemDb= _context.CartItems.FirstOrDefault(ci=>ci.ShoppingCartId==cartItems.ShoppingCartId && ci.VariantId==cartItems.VariantId);
            return cartItemDb;
        }

        public async Task<IEnumerable<CartItems?>> GetCartItemsByShoppingCartIdAsync(int id)
        {
             return await _context.CartItems.Where(ci=>ci.ShoppingCartId==id).ToListAsync();
        }

        public async Task<CartItems?> GetCartItemsById(int id)
        {
           return await _context.CartItems.FindAsync(id);
        }
    }
}