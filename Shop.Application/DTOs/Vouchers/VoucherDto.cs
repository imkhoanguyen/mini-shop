namespace Shop.Application.DTOs.Vouchers
{
    public class VoucherDto
    {
        public string? title { get; set; }
        public string? description { get; set; }
        public decimal percentage { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public bool is_active { get; set; }

        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        
    }

    

}