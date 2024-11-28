using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Shop.Domain.Entities;

namespace Shop.Application.Repositories
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<List<Address>> GetAddressByUserIdAsync(string userId);
        Task<Address> GetByIdAsync(int addressId);
        Task UpdateAsync(Address address);  // Make this async
        Task DeleteAsync(Address address);
        Task<Address?> FindAsync(Expression<Func<Address, bool>> predicate);
        
    }
}