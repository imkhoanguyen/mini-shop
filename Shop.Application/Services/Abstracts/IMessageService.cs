using Shop.Application.DTOs.Messages;
using Shop.Domain.Entities;

namespace Shop.Application.Services.Abstracts
{
    public interface IMessageService
    {
        Task<MessageDto> AddMessageAsync(MessageAdd messageAdd, string claimValue);
        Task<MessageDto> ReplyMessageAsync(MessageAdd messageAdd, string claimValue);
        Task<List<string>> GetUsersByClaimValueAsync(string hasClaim);
        Task<List<string>> GetUsersWithoutClaimAsync(string hasClaim);
        Task<MessageDto> GetLastMessageAsync(string userId);
        Task<IEnumerable<MessageDto>> GetMessageThread(string customerId);
    }
}
