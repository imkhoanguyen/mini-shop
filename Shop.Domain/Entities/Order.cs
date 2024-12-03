using Shop.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Domain.Entities
{
    public class Order : BaseEntity
    {
        // address & user information
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public required string UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser? AppUser { get; set; }
        public string? FullName { get; set; }

        // discount information
        public Discount? Discount { get; set; }
        public int? DiscountId { get; set; }
        public decimal? DiscountPrice { get; set; }

        // shipping method information
        public decimal ShippingFee { get; set; }
        public int ShippingMethodId { get; set; }
        [ForeignKey("ShippingMethodId")]
        public ShippingMethod? ShippingMethod { get; set; }

        // order information
        public decimal SubTotal { get; set; } // gía tất cả sản phẩm
        public OrderStatus Status { get; set; } = OrderStatus.Unconfirmed;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; }
        public List<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
        public string? Description { get; set; }

        //payment method
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Offline;
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

        public string? StripeSessionId { get; set; }

        public decimal GetTotal()
        {
            // giá tất cả sản phẩm + phí ship - giả giảm
            return SubTotal + ShippingFee - DiscountPrice ?? 0;
        }
    }
}