using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class CartItems : BaseEntity
    {
        [Required]
        public int ProductId {get;set;}
        [ForeignKey("ProductId")]
        public Product? Product{get;set;}
        public int ShoppingCartId{get;set;}
        
        [ForeignKey("ShoppingCartId")]
        public ShoppingCart? ShoppingCart{get;set;}
        public int Quantity{get;set;}
        public decimal Price{get;set;}

    }
}