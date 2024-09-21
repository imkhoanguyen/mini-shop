using System.ComponentModel.DataAnnotations;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly StoreContext _context;
        public async Task AddShoppingCart(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Add(shoppingCart);
            await _context.SaveChangesAsync();

        }
        public async Task UpdateShoppingCart (ShoppingCart shoppingCart)
        {
            var shoppingCart=await _context.ShoppingCarts
                .include(sc=> sc.ShoppingCartRepository)
                .inclide(sc=> sc.CartItems)
        }
    }
    
}