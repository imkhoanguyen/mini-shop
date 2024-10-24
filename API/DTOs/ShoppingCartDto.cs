using System.ComponentModel.DataAnnotations;
using API.Entities;

namespace API.DTOs
{
    public class ShoppingCartDto
    {
        public int Id { get; set; }
        public static ShoppingCart toShoppingCart(ShoppingCartDto shoppingCartDto)
        {
            return new ShoppingCart{
                Id=shoppingCartDto.Id
            };
        }
    }
    public class ShoppingCartAddDto
    {
        
        // public DateTime Created { get; set; }=DateTime.UtcNow;
        // public DateTime? Updated {get;set;}
        public static ShoppingCart toShoppingCart(ShoppingCartAddDto shoppingCartAddDto)
        {
            return new ShoppingCart{
                // Created=shoppingCartAddDto.Created,
                // Updated=null
            };
        }
    }
    public class ShoppingCartUpdateDto
    {
        public DateTime Created{get;set;}
        
        
    }
    public class ShoppingCartGetDto
    {
        public int Id{get;set;}
        public List<CartItemsGetByShoppingCartIdDto> CartItems {get;set;}
    }
}