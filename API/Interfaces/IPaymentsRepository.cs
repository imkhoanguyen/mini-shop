using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IPaymentsRepository
    {
        void AddPayments(Payments payments);
        void UpdatePayments(Payments payments);
        void DeletePayments(Payments payments);
    }
}