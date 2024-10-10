namespace API.DTOs
{
    public class ShippingMethodDto
    {
        public string? name { get; set; }
        public decimal? cost { get; set; }
        public DateTime? estimatedDeliveryTime { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}