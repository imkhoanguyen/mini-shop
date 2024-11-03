using Shop.Application.DTOs.Messages;
using Shop.Application.Repositories;
using Shop.Domain.Entities;

namespace API.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<IEnumerable<MessageDto?>> GetMessageThread(string senderId, string recipientId, int skip, int take);
        Task<string> GetUserRoleById(string userId);
        Task<MessageDto?> GetLastMessage(string senderId, string recipientId);
    }
}