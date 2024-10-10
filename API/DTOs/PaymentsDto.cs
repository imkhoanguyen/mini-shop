namespace API.DTOs
{
    public class PaymentsDto
    {
        public int? order_id { get; set; }
        public string? payment_method { get; set; }
        public int? status { get; set; }
        public DateTime? payment_date { get; set; }
    }
}