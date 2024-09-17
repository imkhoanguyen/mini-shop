using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class CartItems
    {
        [Required]
        public int ProductId {get;set;}
        public int ShoppingCartId{get;set;}
        [ForeignKey("ProductId")]
        public Product? Product{get;set;}
        [ForeignKey("ShoppingCartId")]
        public ShoppingCart? ShoppingCart{get;set;}
        public int Quantity{get;set;}
        public Decimal Price{get;set;}

    }
}