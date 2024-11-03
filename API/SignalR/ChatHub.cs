using Microsoft.AspNetCore.SignalR;
using Shop.Domain.Entities;
namespace API.SignalR
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Message message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"User Connected: {Context.UserIdentifier}, Connection ID: {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }

    }
}