using Shop.Application.Parameters;
using Shop.Application.Repositories;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;

namespace API.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<PagedList<Message?>> GetMessageThread(MessageParams messageParams, string customerId);
        Task<Message> GetLastMessageAsync(string customerId);
        Task<List<string>> GetRoleWithClaim(string claimValue);
    }
}