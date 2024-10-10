using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly StoreContext _context;
        public VoucherRepository(StoreContext context)
        {
            _context = context;
        }

        public void AddVoucher(Voucher Voucher)
        {
             _context.Vouchers.Add(Voucher);
        }

        public IEnumerable<Voucher> GetAllVouchers()
        {
            return _context.Vouchers.ToList();
        }


        public Voucher GetVoucher(int voucherId) 
        {
            return _context.Vouchers.Find(voucherId);
        }

        public void UpdateVoucher(Voucher Voucher)
        {
            var existingVoucher = _context.Vouchers.Find(Voucher.Id); // Assuming Id is the primary key

            if (existingVoucher != null)
            {
                existingVoucher.title = Voucher.title;
                existingVoucher.description = Voucher.description;
                existingVoucher.percentage = Voucher.percentage;
                existingVoucher.start_date = Voucher.start_date;
                existingVoucher.end_date = Voucher.end_date;
                existingVoucher.is_active = Voucher.is_active;
                existingVoucher.updated_at = DateTime.Now; // Update the timestamp
            }
        }
        public void DeleteVoucher(int voucherId)
        {
            var voucher = _context.Vouchers.FirstOrDefault(v => v.Id == voucherId);
            if (voucher != null)
            {
                voucher.is_active = false; 
                _context.Vouchers.Update(voucher);
            }
        }
        
        public void RestoreVoucher(int voucherId)
        {
            var voucher = _context.Vouchers.FirstOrDefault(v => v.Id == voucherId);
            if (voucher != null)
            {
                voucher.is_active = true; 
                _context.Vouchers.Update(voucher);
            }
        }

        
    }
}



        
        