using Microsoft.AspNetCore.Http;
using Shop.Application.DTOs.Messages;

namespace Shop.Application.Services.Abstracts
{
    public interface IMessageService
    {
        Task<MessageDto> AddMessageAsync(MessageAdd messageAdd);
        Task<MessageDto> ReplyMessageAsync(MessageAdd messageAdd);
        Task<List<string>> GetUsersByClaimValueAsync(string claimValue);
        Task<MessageDto> GetLastMessageAsync(string senderId, string recipientId);
        Task<IEnumerable<MessageDto>> GetMessageThread(string senderId, string recipientId, int skip, int take);
        Task AddFileAsync(IFormFileCollection files);
    }
}
