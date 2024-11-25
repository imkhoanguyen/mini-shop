using Shop.Application.DTOs.Messages;
using Shop.Application.Parameters;
using Shop.Application.Ultilities;
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
        Task<PagedList<MessageDto>> GetMessageThread(MessageParams messageParams, string customerId);
    }
}
