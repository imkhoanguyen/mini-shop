using Microsoft.AspNetCore.Http;
using Shop.Application.DTOs.Messages;
using Shop.Application.DTOs.Users;
using Shop.Domain.Entities;

namespace Shop.Application.Services.Abstracts
{
    public interface IMessageService
    {
        Task<MessageDto> AddMessageAsync(MessageAdd messageAdd);
        Task<MessageDto> ReplyMessageAsync(MessageAdd messageAdd);
        Task<List<UserDto>> GetUsersByClaimValueAsync(string claimValue);
        Task<IEnumerable<MessageDto>> GetMessageThread(string senderId, string recipientId, int skip, int take);
        Task<MessageDto> GetLastMessage(string senderId, string recipientId);
        Task AddFileAsync(IFormFileCollection files);
    }
}
