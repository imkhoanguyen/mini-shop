
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Parameters;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;
using Shop.Infrastructure.DataAccess;
using Shop.Infrastructure.Repositories;

namespace API.Repositories
{
    public class VoucherRepository : Repository<Voucher>, IVoucherRepository
    {
        private readonly StoreContext _context;
        public VoucherRepository(StoreContext context) : base(context)
        {
            _context = context;
        }

        public async Task UpdateVoucherAsync(Voucher Voucher)
        {
            var existingVoucher = await _context.Vouchers.FindAsync(Voucher.Id); // Assuming Id is the primary key

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
        public async Task DeleteVoucherAsync(int voucherId)
        {
            var voucher = await _context.Vouchers.FirstOrDefaultAsync(v => v.Id == voucherId);
            if (voucher != null)
            {
                voucher.is_active = false;
            }
        }

        public async Task RestoreVoucherAsync(int voucherId)
        {
            var voucher = await _context.Vouchers.FirstOrDefaultAsync(v => v.Id == voucherId);
            if (voucher != null)
            {
                voucher.is_active = true;
            }
        }


        public async Task<IEnumerable<Voucher>> GetAllVouchers(bool tracked = false)
        {
            if (tracked)
                return await _context.Vouchers.Where(v => !v.is_active).ToListAsync();
            return await _context.Vouchers.AsNoTracking().Where(v => !v.is_active).ToListAsync();
        }

        public Task<PagedList<Voucher>> GetAllVouchersAsync(VoucherParams prm, bool tracked = false)
        {
            throw new NotImplementedException();
        }
    }
}




