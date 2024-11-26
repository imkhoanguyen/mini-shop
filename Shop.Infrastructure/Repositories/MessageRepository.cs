using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Parameters;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;
using Shop.Infrastructure.DataAccess;
using Shop.Infrastructure.Ultilities;
using System.Diagnostics;

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


        public async Task<PagedList<Message?>> GetMessageThread(MessageParams messageParams, string customerId)
        {
            var query = _context.Messages
                .Include(m => m.Files)
                .Where(m =>
                    m.SenderId == customerId || m.RecipientIds.Contains(customerId)) 
                .AsQueryable();

            if (!string.IsNullOrEmpty(messageParams.Search))
            {
                query = query.Where(c =>
                    c.Content.ToLower().Contains(messageParams.Search.ToLower()) ||
                    c.Id.ToString() == messageParams.Search);
            }
            if (!string.IsNullOrEmpty(messageParams.OrderBy))
            {
                query = messageParams.OrderBy switch
                {
                    "id" => query.OrderBy(c => c.Id),
                    "id_desc" => query.OrderByDescending(c => c.Id),
                    _ => query.OrderByDescending(c => c.Id)
                };
            }

            return await query.ApplyPaginationAsync(messageParams.PageNumber, messageParams.PageSize);
        }

    }
}