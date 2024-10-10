using System.ComponentModel.DataAnnotations.Schema;
using API.Data.Enum;
using API.DTOs;

namespace API.Entities
{
    public class Payments : BaseEntity
    {
       
        public int? order_id { get; set; }
        [ForeignKey("order_id")]
        public Order? Order { get; set; }
        public string? payment_method { get; set; }
        public int? status { get; set; }
        public DateTime? payment_date { get; set; }
        
        public static PaymentsDto toPaymentsDto(Payments Payments)
        {
            return new PaymentsDto
            {
                order_id=Payments.order_id,
                payment_method=Payments.payment_method,
                status=Payments.status,
                payment_date=Payments.payment_date

            };
        }
    }
}