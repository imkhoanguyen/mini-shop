using Shop.Application.DTOs.Messages;
using Shop.Application.Repositories;
using Shop.Domain.Entities;

namespace API.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<IEnumerable<Message?>> GetMessageThread(string customerId);
        Task<Message> GetLastMessageAsync(string customerId);
        Task<List<string>> GetRoleWithClaim(string claimValue);
    }
}