using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{
    public class MessageHub : Hub
    {
        private readonly IHubContext<PresenceHub> _presenceHub;
        private readonly IUnitOfWork _unitOfWork;
        public MessageHub(IHubContext<PresenceHub> presenceHub, IUnitOfWork unitOfWork)
        {
            _presenceHub = presenceHub;
            _unitOfWork = unitOfWork;
        }
       
    }
}