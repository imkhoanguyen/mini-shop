using API.Entities;

namespace API.Interfaces
{
    public interface IVoucherRepository
    {
        Voucher GetVoucher(int voucherId);
        IEnumerable<Voucher> GetAllVouchers();
        void AddVoucher(Voucher Voucher);
        void UpdateVoucher(Voucher Voucher);
        void DeleteVoucher(int id);
       
        void RestoreVoucher(int id);
    }
}