using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shop.Application.DTOs.Messages;
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
               .Distinct()
               .ToListAsync();

            return rolesWithClaim;
        }

        //public async Task<Message?> GetLastMessage(string senderId, string recipientId)
        //{
        //    return await _context.Messages
        //        .Where(m => (m.Sender!.Id == senderId && m.Recipient!.Id == recipientId) ||
        //                    (m.Sender.Id == recipientId && m.Recipient!.Id == senderId))
        //        .OrderByDescending(m => m.SentAt)
        //        .FirstOrDefaultAsync();
        //}

        //public async Task<IEnumerable<Message?>> GetMessageThread(string senderId, string recipientId, int skip, int take)
        //{
        //var messages = await _context.Messages
        //    .Where(m => (m.SenderId == senderId && m.RecipientIds == recipientId) ||
        //                (m.SenderId == recipientId && m.RecipientId == senderId))
        //    .OrderByDescending(m => m.SentAt)
        //    .Skip(skip)
        //    .Take(take)
        //    .ToListAsync();

        //return messages;
        //}

    }
}