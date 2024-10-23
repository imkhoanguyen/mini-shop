using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.SignalR;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers
{
    public class MessagesController : BaseApiController
    {
        private readonly IMessageRepository _message;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<ChatHub> _hub;
        private readonly IImageService _fileService;
        public MessagesController(IMessageRepository message, IUnitOfWork unitOfWork, IHubContext<ChatHub> hub, IImageService fileService)
        {
            _message = message;
            _unitOfWork = unitOfWork;
            _hub = hub;
            _fileService = fileService;
        }
        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] MessageAddDto messageDto)
        {
           
            var message = await MessageAddDto.ToMessageAsync(messageDto);
            _message.AddMessage(message);
            if (await _unitOfWork.Complete())
            {
                return Ok(new { message = "Message sent successfully" });
            }
            return BadRequest(new { message = "Message sent failed." });
        }

        [HttpGet("GetMessageThread")]
        public async Task<IActionResult> GetMessageThread(string senderId, string recipientId, int skip, int take)
        {
            if (skip < 0 || take <= 0)
            {
                return BadRequest("Invalid skip or take parameters.");
            }
            var messages = await _message.GetMessageThread(senderId, recipientId, skip, take);
            return Ok(messages);
        }
        [HttpGet("GetLastMessage")]
        public async Task<IActionResult> GetLastMessage(string senderId, string recipientId)
        {
            var message = await _message.GetLastMessage(senderId, recipientId);
            return Ok(message);
        }

    }
}