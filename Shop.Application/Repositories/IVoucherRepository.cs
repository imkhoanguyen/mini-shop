using Shop.Application.Parameters;
using Shop.Application.Repositories;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;

namespace API.Interfaces
{
    public interface IVoucherRepository : IRepository<Voucher>
    {
        Task UpdateVoucherAsync(Voucher Voucher);
        Task DeleteVoucherAsync(int id);
        Task RestoreVoucherAsync(int id);
        Task<IEnumerable<Voucher>> GetAllVouchers(bool tracked = false);
        Task<PagedList<Voucher>> GetAllVouchersAsync(VoucherParams prm, bool tracked = false);
    }
}