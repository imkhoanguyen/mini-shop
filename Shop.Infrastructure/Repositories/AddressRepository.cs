using Shop.Application.Repositories;
using Shop.Domain.Entities;
using Shop.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Shop.Infrastructure.Repositories
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        private readonly StoreContext _context;
        public AddressRepository(StoreContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Address>> GetAddressByUserIdAsync(string userId)
{
            return await _context.Addresses
                .AsNoTracking() // Thêm để tối ưu khi chỉ đọc
                .Where(address => address.AppUserId == userId)
                .ToListAsync();
        }

        public async Task<Address> GetByIdAsync(int addressId)
        {
            return await _context.Addresses
                .AsNoTracking()
                .FirstOrDefaultAsync(address => address.Id == addressId);
        }

        public async Task UpdateAsync(Address address)
        {
            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();  // Make sure to save asynchronously
        }

        public async Task DeleteAsync(Address address)
        {
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();  // Make sure to save asynchronously
        }
        public async Task<Address?> FindAsync(Expression<Func<Address, bool>> predicate)
        {
            return await _context.Addresses.FirstOrDefaultAsync(predicate);
        }
    }
}