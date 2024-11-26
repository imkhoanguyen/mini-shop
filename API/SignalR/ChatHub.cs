using Microsoft.AspNetCore.SignalR;
using Shop.Application.DTOs.Messages;
using System.Collections.Concurrent;
namespace API.SignalR
{
    public class ChatHub : Hub
    {
        private static readonly ConcurrentDictionary<string, string> ActiveAdmins = new();

        public async Task JoinGroup(string recipientId, string senderId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, recipientId);
            await Groups.AddToGroupAsync(Context.ConnectionId, senderId);
        }

        public async Task NotifyTyping(string customerId, string adminId)
        {
            if (ActiveAdmins.ContainsKey(customerId) && ActiveAdmins[customerId] != adminId)
            {
                await Clients.Caller.SendAsync("TypingBlocked", ActiveAdmins[customerId]);
                return;
            }

            ActiveAdmins[customerId] = adminId;
            await Clients.OthersInGroup(customerId).SendAsync("ReceiveTypingStatus", true, customerId, adminId);
        }
        public async Task StopTyping(string customerId, string adminId)
        {
            if (ActiveAdmins.TryGetValue(customerId, out var activeAdminId) && activeAdminId == adminId)
            {
                ActiveAdmins.TryRemove(customerId, out _);
                await Clients.OthersInGroup(customerId).SendAsync("ReceiveTypingStatus", false, customerId, adminId);
            }

        }
        public async Task SendMessage(MessageDto message)
        {
            if (message == null || string.IsNullOrWhiteSpace(message.SenderId))
            {
                throw new ArgumentException("Message or sender ID is invalid.");
            }

            await Clients.Group(message.SenderId).SendAsync("ReceiveMessage", message);
            Console.WriteLine($"Message sent to sender: {message.SenderId}");

            if (message.RecipientIds != null && message.RecipientIds.Any())
            {
                var tasks = message.RecipientIds
                    .Select(recipientId =>
                    {
                        Console.WriteLine($"Sending message to recipient: {recipientId}");
                        return Clients.Group(recipientId).SendAsync("ReceiveMessage", message);
                    });
                await Task.WhenAll(tasks);
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var customerIdsToRemove = ActiveAdmins
                .Where(kv => kv.Value == Context.ConnectionId)
                .Select(kv => kv.Key)
                .ToList();

            foreach (var customerId in customerIdsToRemove)
            {
                ActiveAdmins.TryRemove(customerId, out _);
            }

            await base.OnDisconnectedAsync(exception);
        }

    }
}