using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Infrastructure.DataAccess;

namespace Shop.Infrastructure.Repositories
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        private readonly StoreContext _context;
        public MessageRepository(StoreContext context) : base(context)
        {
            _context = context;

        }

        public async Task<List<string>> GetRoleWithClaim(string claimValue)
        {
            var rolesWithClaim = await _context.RoleClaims
               .Where(rc => rc.ClaimType == "Permission" && rc.ClaimValue == claimValue)
               .Select(rc => rc.RoleId)
               .ToListAsync();

            return rolesWithClaim;
        }

        public async Task<Message?> GetLastMessageAsync(string customerId)
        {
            return await _context.Messages
                .Include(m => m.Files)
                .Where(m => (m.SenderId == customerId || 
                    m.RecipientIds != null && m.RecipientIds.Contains(customerId)))
                .OrderByDescending(m => m.SentAt)
                .FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<Message?>> GetMessageThread(string customerId)
        {
            return await _context.Messages
                .Include(m => m.Files)
                .Where(m =>
                    (m.SenderId == customerId  || m.RecipientIds!.Contains(customerId))
                )
                .OrderBy(m => m.SentAt)
                .ToListAsync();
                }

    }
}