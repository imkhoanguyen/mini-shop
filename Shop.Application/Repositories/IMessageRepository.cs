using Shop.Application.DTOs.Messages;
using Shop.Application.Repositories;
using Shop.Domain.Entities;

namespace API.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        //Task<IEnumerable<Message?>> GetMessageThread(string senderId, string recipientId, int skip, int take);
        //Task<Message?> GetLastMessage(string senderId, string recipientId);
        Task<List<string>> GetRoleWithClaim(string claimValue);
    }
}