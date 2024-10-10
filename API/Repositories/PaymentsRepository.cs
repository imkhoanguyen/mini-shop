using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authentication;

namespace API.Repositories
{
    public class PaymentsRepository : IPaymentsRepository
    {
        private readonly StoreContext _context;
        public PaymentsRepository (StoreContext context)
        {
            _context=context;
        }
        public void AddPayments(Payments payments)
        {
            _context.Payments.Add(payments);
        }

        public void DeletePayments(Payments payments)
        {
            var paymentsDb =_context.Payments.FirstOrDefault(pm=>pm.Id==payments.Id);
            if(paymentsDb is not null)
            {
                paymentsDb.Status=0;
            }
        }

        public void UpdatePayments(Payments payments)
        {
            var paymentsDb=_context.Payments.FirstOrDefault(pm=>pm.Id==payments.Id);
            if(paymentsDb is not null)
            {
                paymentsDb.OrderId=payments.OrderId;
                paymentsDb.Payment_method=payments.Payment_method;
                paymentsDb.Payment_date=payments.Payment_date;
            }
        }
    }
}