using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Domain.Entities
{
    [NotMapped]
    public class ShoppingCart
    {
        public required string Id { get; set; }
        public List<CartItem> Items { get; set; } = [];
    }
}
