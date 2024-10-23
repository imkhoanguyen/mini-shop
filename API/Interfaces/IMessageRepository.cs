using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IMessageRepository
    {
        void AddMessage(Message message);
        Task<IEnumerable<MessageDto?>> GetMessageThread(string senderId, string recipientId, int skip, int take);
        Task<string> GetUserRoleById(string userId);
        Task<Message?> GetLastMessage(string senderId, string recipientId);
    }
}