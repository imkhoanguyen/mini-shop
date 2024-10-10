using System.Reflection.Metadata.Ecma335;
using API.Entities;

namespace API.DTOs
{
    public class PaymentsDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public required string Payment_method { get; set; }
        public DateTime Payment_Date { get; set; }
        public int Status { get; set; }
        public static Payments toPayments(PaymentsDto paymentsDto)
        {
            return new Payments
            {
                Id=paymentsDto.Id,
                OrderId=paymentsDto.OrderId,
                Payment_method=paymentsDto.Payment_method,
                Payment_date=paymentsDto.Payment_Date,
                Status=paymentsDto.Status
            };
        }
    }
    public class PaymentsAddDto
    {
        public int OrderId { get; set; }
        public int Status { get; set; }
        public required string Payment_method { get; set; }
        public DateTime Payments_Date { get; set; }
        public static Payments toPayments(PaymentsAddDto paymentsAddDto)
        {
            return new Payments
            {
                OrderId=paymentsAddDto.OrderId,
                Status=1,
                Payment_method=paymentsAddDto.Payment_method,
                Payment_date=paymentsAddDto.Payments_Date
            };
        }
    }
    public class PaymentsUpdateDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public required string Payment_method { get; set; }
        public DateTime Payment_Date{get;set;}
        public static Payments toPayments(PaymentsUpdateDto paymentsUpdateDto)
        {
            return new Payments
            {
                Id=paymentsUpdateDto.Id,
                OrderId=paymentsUpdateDto.OrderId,
                Payment_method=paymentsUpdateDto.Payment_method
            };
        }
    }
    public class PaymentsDeleteDto
    {
        public int Id { get; set; }
        public static Payments toPayments(PaymentsDeleteDto paymentsDeleteDto)
        {
            return new Payments
            {
                Id=paymentsDeleteDto.Id
            };
        }
    }
    
}