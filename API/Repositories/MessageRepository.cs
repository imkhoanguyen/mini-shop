using API.Data;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly StoreContext _context;
        public MessageRepository(StoreContext context)
        {
            _context = context;

        }
        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
        }

        public async Task<IEnumerable<MessageDto?>> GetMessageThread(string senderId, string recipientId, int skip, int take)
        {
            var messages = await _context.Messages
                .Where(m => (m.Sender!.Id == senderId && m.Recipient!.Id == recipientId) ||
                            (m.Sender.Id == recipientId && m.Recipient!.Id == senderId))
                .OrderBy(m => m.SentAt)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return messages.Select(m => Message.toMessageDto(m));
        }
        public async Task<string> GetUserRoleById(string userId)
        {
            var roleName = await _context.UserRoles
                .Where(u => u.UserId == userId)
                .Select(u => u.RoleId)
                .Join(_context.Roles,
                        roleId => roleId,
                        role => role.Id,
                        (roleId, role) => role.Name)
                .FirstOrDefaultAsync();
            return roleName ?? "No role assigned";
        }
        public async Task<Message?> GetLastMessage(string senderId, string recipientId)
        {
            return await _context.Messages
                .Where(m => (m.Sender!.Id == senderId && m.Recipient!.Id == recipientId) ||
                            (m.Sender.Id == recipientId && m.Recipient!.Id == senderId))
                .OrderByDescending(m => m.SentAt)
                .FirstOrDefaultAsync();
        }
    }
}