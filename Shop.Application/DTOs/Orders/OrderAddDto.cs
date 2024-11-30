using Shop.Domain.Entities;
using Shop.Domain.Enum;
using Shop.Domain.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace Shop.Application.DTOs.Orders
{
    public class OrderAddDto
    {
        // address & user information
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ nhận hàng")]
        public required string Address { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại người nhận hàng")]
        [VietnamPhoneNumber]
        public required string Phone { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên người nhận hàng")]
        public required string FullName { get; set; }
        public string? UserId { get; set; }

        // shipping method information
        public decimal ShippingFee { get; set; }
        public int ShippingMethodId { get; set; }


        // discount information
        public int? DiscountId { get; set; }
        public decimal? DiscountPrice { get; set; }

        // order information
        public string? Description { get; set; }
        public decimal SubTotal { get; set; }


        public required string PaymentMethod { get; set; }
        public string? StripeSessionId { get; set; }

        // list item
        [NotEmptyList(ErrorMessage = "Vui lòng thêm sản phẩm vào giỏ hàng")]
        public List<CartItem> Items { get; set; } = [];
    }
}