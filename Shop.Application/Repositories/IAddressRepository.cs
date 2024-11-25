using Shop.Domain.Entities;

namespace Shop.Application.Repositories
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<List<Address>> GetAddressByUserIdAsync(string userId);
        Task<Address> GetByIdAsync(int addressId);
        Task UpdateAsync(Address address);  // Make this async
        Task DeleteAsync(Address address);
        
    }
}