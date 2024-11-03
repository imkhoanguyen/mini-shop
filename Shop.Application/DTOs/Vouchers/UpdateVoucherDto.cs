using Shop.Domain.Entities;

namespace Shop.Application.DTOs.Vouchers
{
    public class UpdateVoucherDto
    {
        public string? title { get; set; } // Optional, can be null for no change
        public string? description { get; set; } // Optional
        public decimal? percentage { get; set; } // Optional
        public DateTime? start_date { get; set; } // Optional
        public DateTime? end_date { get; set; } // Optional
        public bool? is_active { get; set; } // Optional

       


    }
}
