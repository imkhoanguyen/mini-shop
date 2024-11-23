using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Interfaces;
using API.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Shop.Application.DTOs.Messages;
using Shop.Application.DTOs.Users;
using Shop.Application.Services.Abstracts;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;

namespace API.Controllers
{
    public class MessagesController : BaseApiController
    {
        private readonly IMessageService _messageService;
        private readonly UserManager<AppUser> _userManager;
        public MessagesController(IMessageService messageService, UserManager<AppUser> userManager)
        {
            _messageService = messageService;
            _userManager = userManager;
        }
        [HttpGet("GetCustomers")]
        public async Task<ActionResult<List<string>>> GetCustomers()
        {
            var customers = await _messageService.GetUsersWithoutClaimAsync(ClaimStore.Message_Reply);
            return Ok(customers);
        }
        [HttpPost("AddMessage")]
        public async Task<IActionResult> AddMessage([FromForm] MessageAdd messageAdd)
        {
            var adminClaim = await _messageService.GetUsersByClaimValueAsync(ClaimStore.Message_Reply);

            var message = adminClaim.Contains(messageAdd.SenderId!)
                ? await _messageService.ReplyMessageAsync(messageAdd, ClaimStore.Message_Reply)
                : await _messageService.AddMessageAsync(messageAdd, ClaimStore.Message_Reply);

            return Ok(message);

        }
        [HttpGet("GetMessageThread")]
        public async Task<IActionResult> GetMessageThread(string customerId)
        {

            var messages = await _messageService.GetMessageThread(customerId);
            return Ok(messages);
        }
        [HttpGet("GetLastMessage")]
        public async Task<IActionResult> GetLastMessage(string userId)
        {
            var message = await _messageService.GetLastMessageAsync(userId);
            return Ok(message);
        }

    }
}