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
        public async Task SendMessage(Message message)
        {
            await _context.Messages.AddAsync(message);
        }

        public async Task<IEnumerable<MessageDto?>> GetMessageThread(string senderId, string repliedById, int skip, int take)
        {
            var messages = await _context.Messages
                .Where(m => (m.SenderId == senderId && m.RecipientIds.Contains(repliedById)) ||
                            (m.SenderId == repliedById && m.RecipientIds.Contains(senderId)))
                .OrderByDescending(m => m.SentAt)
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
        public async Task<Message?> GetLastMessage(string senderId, string repliedById)
        {
            return await _context.Messages
                .Where(m => (m.Sender!.Id == senderId && m.RepliedById == repliedById) ||
                            (m.Sender.Id == repliedById && m.RepliedById == senderId))
                .OrderByDescending(m => m.SentAt)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ReplyMessage(int messageId, string repliedById)
        {
            var repliedMessage = await _context.Messages.FindAsync(messageId);
            if(repliedMessage != null && !repliedMessage!.IsReplied){
                repliedMessage!.IsReplied = true;
                repliedMessage.RepliedById = repliedById;

                return true;
            } else {
                return false;
            }
        }
        public async Task<List<Message>> GetMessagesForEmployee(string employeeId)
        {
            return await _context.Messages
                .Where(m => !m.IsReplied || m.RepliedById == employeeId)
                .ToListAsync();
        }
        public async Task<Message?> GetMessageById(int messageId)
        {
            return await _context.Messages
                .Where(m => m.Id == messageId)
                .FirstOrDefaultAsync();
        }
    }
}