using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IMessageRepository
    {
        void AddMessage(Message message);
        void DeleteMessage(Message message);
        Task<Message?> GetMessage(int id);
        Task<PageList<Message?>> GetMessagesForUser(MessageParams messageParams);
        Task<IEnumerable<Message?>> GetMessageThread(string currentUsername, string recipientUsername);
    }
}