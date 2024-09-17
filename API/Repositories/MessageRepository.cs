using API.Data;
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

        public void DeleteMessage(Message message)
        {
            _context.Messages.Remove(message);
        }

        public async Task<Message?> GetMessage(int id)
        {
            return await _context.Messages.FindAsync(id);
        }

        public async Task<PageList<Message?>> GetMessagesForUser(MessageParams messageParams)
        {
            var query = _context.Messages
            .OrderByDescending(m => m.MessageSend).AsQueryable();
            query = messageParams.Container switch
            {
                "Inbox" => query.Where(u => u.RecipientUserName == messageParams.UserName),
                "Outbox" => query.Where(u => u.SenderUserName == messageParams.UserName),
                _ => query.Where(u => u.RecipientUserName == messageParams.UserName && u.DateRead == null)
            };
            var count = await query.CountAsync();
            var messages = await query.Skip((messageParams.PageNumber - 1) * messageParams.PageSize)
                                   .Take(messageParams.PageSize)
                                   .Include(m => m.Sender)
                                   .Include(m => m.Recipient)
                                   .ToListAsync();
            return new PageList<Message?>(messages, count, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<IEnumerable<Message?>> GetMessageThread(string currentUserName, string recipientUserName)
        {
            var query = _context.Messages
                .Where(
                m => m.RecipientUserName == currentUserName
                && m.SenderUserName == recipientUserName
                || m.RecipientUserName == recipientUserName
                && m.SenderUserName == currentUserName
                ).OrderBy(m => m.MessageSend).AsQueryable();

            var unreadMessages = query.Where(m => m.DateRead == null && m.RecipientUserName == currentUserName).ToList();

            if (unreadMessages.Any())
            {
                foreach (var message in unreadMessages)
                {
                    message.DateRead = DateTime.UtcNow;
                }
            }
            return await query.ToListAsync();
        }
    }
}