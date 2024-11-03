using API.Interfaces;
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

        public Task<MessageDto?> GetLastMessage(string senderId, string recipientId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MessageDto?>> GetMessageThread(string senderId, string recipientId, int skip, int take)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserRoleById(string userId)
        {
            throw new NotImplementedException();
        }

        //public async Task<IEnumerable<MessageDto?>> GetMessageThread(string senderId, string recipientId, int skip, int take)
        //{
        //    var messages = await _context.Messages
        //        .Where(m => (m.Sender!.Id == senderId && m.Recipient!.Id == recipientId) ||
        //                    (m.Sender.Id == recipientId && m.Recipient!.Id == senderId))
        //        .OrderByDescending(m => m.SentAt)
        //        .Skip(skip)
        //        .Take(take)
        //        .ToListAsync();

        //    return messages.Select(m => Message.toMessageDto(m));
        //}
        //public async Task<string> GetUserRoleById(string userId)
        //{
        //    var roleName = await _context.UserRoles
        //        .Where(u => u.UserId == userId)
        //        .Select(u => u.RoleId)
        //        .Join(_context.Roles,
        //                roleId => roleId,
        //                role => role.Id,
        //                (roleId, role) => role.Name)
        //        .FirstOrDefaultAsync();
        //    return roleName ?? "No role assigned";
        //}
        //public async Task<Message?> GetLastMessage(string senderId, string recipientId)
        //{
        //    return await _context.Messages
        //        .Where(m => (m.Sender!.Id == senderId && m.Recipient!.Id == recipientId) ||
        //                    (m.Sender.Id == recipientId && m.Recipient!.Id == senderId))
        //        .OrderByDescending(m => m.SentAt)
        //        .FirstOrDefaultAsync();
        //}
    }
}