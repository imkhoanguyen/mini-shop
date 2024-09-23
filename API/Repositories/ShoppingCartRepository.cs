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
        public ShoppingCartRepository(StoreContext context)
        {
            _context=context;
        }
        public void AddShoppingCart(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Add(shoppingCart);

        }
        public void DeleteShoppingCart(ShoppingCart shoppingCart)
        {
            throw new NotImplementedException();
        }

        public void UpdateShopingCart(ShoppingCart shoppingCart)
        {
            
        }

        
        public async Task UpdateShoppingCart (ShoppingCart shoppingCart)
        {
            


        }

        void IShoppingCartRepository.AddShoppingCart(ShoppingCart shoppingCart)
        {
            throw new NotImplementedException();
        }
    }
    
}