using System.ComponentModel.DataAnnotations.Schema;
using API.Data.Enum;
using API.DTOs;

namespace API.Entities
{
    public class Voucher : BaseEntity
    {
       
        public string? title { get; set; }
        public string? description { get; set; }
        public decimal percentage { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; } 
        public bool is_active { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        
        public static VoucherDto toVoucherDto(Voucher voucher)
        {
            return new VoucherDto
            {
                title=voucher.title,
                description=voucher.description,
                percentage=voucher.percentage,
                start_date=voucher.start_date,
                end_date=voucher.end_date,
                is_active=voucher.is_active,
                created_at=voucher.created_at,
                updated_at=voucher.updated_at
            };
        }
    }
}