using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly StoreContext _context;
        public UserRepository(StoreContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<AppUser>> GetUsersWithAdminRole()
        {
            var adminRole = _context.Roles.FirstOrDefault(r => r.Name == "Admin" || r.Name == "admin");
            if(adminRole is null)  return Enumerable.Empty<AppUser>();

            var adminUsers = await _context.UserRoles
                .Where(ur => ur.RoleId == adminRole.Id) 
                .Join(_context.Users,
                        userRole => userRole.UserId,
                        user => user.Id,
                        (userRole, user) => user) 
                .ToListAsync(); 

            return adminUsers;
        }

        public async Task<IEnumerable<AppUser>> GetUsersWithCustomerRole()
        {
            var customerRole = _context.Roles.FirstOrDefault(r => r.Name == "Customer" || r.Name == "customer");
            if(customerRole is null)  return Enumerable.Empty<AppUser>();

            var customerUsers = await _context.UserRoles
                .Where(ur => ur.RoleId == customerRole.Id) 
                .Join(_context.Users,
                        userRole => userRole.UserId,
                        user => user.Id,
                        (userRole, user) => user) 
                .ToListAsync(); 

            return customerUsers;
        }
    }
}