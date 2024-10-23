using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetUsersWithAdminRole();
        Task<IEnumerable<AppUser>> GetUsersWithCustomerRole();
    }
}