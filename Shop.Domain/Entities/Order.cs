using Shop.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Domain.Entities
{
    public class Order : BaseEntity
    {
        public decimal SubTotal { get; set; }
        public DateTime Order_date { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public decimal ShippingFee { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; }
        public int ShippingMethodId { get; set; }
        [ForeignKey("ShippingMethodId")]
        public ShippingMethod? ShippingMethod { get; set; }
        public List<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
        public required string UserId { get; set; }
        public AppUser? AppUser { get; set; }

        public Discount? Discount { get; set; }
        public int DiscountId { get; set; }

        public decimal DiscountPrice { get; set; }

        public decimal GetTotal()
        {
            return (SubTotal + ShippingFee) - (SubTotal + ShippingFee) * DiscountPrice / 100;
        }
    }
}