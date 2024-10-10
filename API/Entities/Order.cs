using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using API.Data.Enum;
using API.DTOs;

namespace API.Entities
{
    public class Order : BaseEntity
    {
       
        public decimal? totalprice { get; set; }
        public int? status { get; set; }
        public DateTime? order_date { get; set; }
        public string? receiver_name { get; set; }
        public string? address { get; set; }
        public string? phone { get; set; }
        public string? user_id { get; set; }
        [ForeignKey("user_id")]
        public AppUser? AppUser { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public int? shipping_method_id { get; set; }
        public decimal? shipping_fee { get; set; }
        
        public static OrderDto toOrderDto(Order Order)
        {
            return new OrderDto
            {   
                totalprice=Order.totalprice,
                status=Order.status,
                order_date=Order.order_date,
                receiver_name=Order.receiver_name,
                address=Order.address,
                phone=Order.phone,
                created_at=Order.created_at,
                updated_at=Order.updated_at,
                shipping_method_id=Order.shipping_method_id,
                shipping_fee=Order.shipping_fee
            };
        }
    }
}