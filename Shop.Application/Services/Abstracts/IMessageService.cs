using Shop.Application.DTOs.Messages;

namespace Shop.Application.Services.Abstracts
{
    public interface IMessageService
    {
        Task<MessageDto> AddMessageAsync(MessageAdd messageAdd);
        Task<IEnumerable<MessageDto>> GetMessageThread(string senderId, string recipientId);
        Task<MessageDto> GetLastMessage(string senderId, string recipientId);
    }
}
