using System.Numerics;

namespace Shop.Application.DTOs.Orders
{
    public class OrderDto
    {
        public decimal? totalprice { get; set; }
        public int? status { get; set; }
        public DateTime? order_date { get; set; }
        public string? receiver_name { get; set; }
        public string? address { get; set; }
        public string? phone { get; set; }
        public int? user_id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public int? shipping_method_id { get; set; }
        public decimal? shipping_fee { get; set; }
    }
}