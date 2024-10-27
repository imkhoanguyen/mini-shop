using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IMessageRepository
    {
        Task SendMessage(Message message);
        Task<IEnumerable<MessageDto?>> GetMessageThread(string senderId, string recipientId, int skip, int take);
        Task<string> GetUserRoleById(string userId);
        Task<Message?> GetLastMessage(string senderId, string recipientId);
        Task<bool> ReplyMessage(int messageId, string repliedById);
        Task<Message?> GetMessageById(int messageId);
        Task<List<Message>> GetMessagesForEmployee(string employeeId);
    }
}