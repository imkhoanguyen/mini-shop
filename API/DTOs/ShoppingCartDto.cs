using System.ComponentModel.DataAnnotations;
using API.Entities;

namespace API.DTOs
{
    public class ShoppingCartDto
    {
        public int Id { get; set; }
        public static ShoppingCart toShoppingCart(ShoppingCartDto shogpingCartDto)
        {
            return new ShoppingCart{
                Id=shogpingCartDto.Id
                
            };
        }
        
    }
    public class ShoppingCartAddDto
    {
        [Required]
        public DateTime Created { get; set; }=DateTime.UtcNow;
        public DateTime? Updated {get;set;}
        public static ShoppingCart toShoppingCart(ShoppingCartAddDto shoppingCartAddDto)
        {
            return new ShoppingCart{
                Created=shoppingCartAddDto.Created,
                Updated=null
            };
        }
    }
    public class ShoppingCartUpdateDto
    {
        public DateTime Created{get;set;}
        
        
    }
}