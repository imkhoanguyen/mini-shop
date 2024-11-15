using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Interfaces;
using API.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Shop.Application.DTOs.Messages;
using Shop.Application.Services.Abstracts;
using Shop.Application.Ultilities;

namespace API.Controllers
{
    public class MessagesController : BaseApiController
    {
        private readonly IMessageService _messageService;
        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        //[HttpPost("UploadFiles")]
        //public async Task<IActionResult> UploadFiles([FromForm] IFormFileCollection files)
        //{
        //    // Nếu không có tệp, trả về response trống
        //    if (files == null || files.Count == 0)
        //    {
        //        return Ok(new { files = new List<object>() });
        //    }

        //    var uploadResults = new List<object>();

        //    foreach (var file in files)
        //    {
        //        var uploadResult = await _fileService.UploadFileAsync(file);
        //        if (uploadResult == null)
        //        {
        //            return BadRequest(new { message = $"File '{file.FileName}' upload failed." });
        //        }

        //        uploadResults.Add(new
        //        {
        //            fileUrl = uploadResult.Url.ToString(),
        //            fileType = file.ContentType
        //        });
        //    }

        //    return Ok(new { files = uploadResults });
        //}

        [HttpPost("AddMessage")]
        public async Task<IActionResult> AddMessage(MessageAdd messageAdd)
        {
            var usersWithClaim = await _messageService.GetUsersByClaimValueAsync(ClaimStore.Message_Reply);
            if (!usersWithClaim.Contains(messageAdd.SenderId!))
            {
                messageAdd.RecipientIds = usersWithClaim;
                var message = await _messageService.AddMessageAsync(messageAdd);
                return Ok(message);
            }
            else if (usersWithClaim.Contains(messageAdd.SenderId!))
            {
                var message = await _messageService.ReplyMessageAsync(messageAdd);
                return Ok(message);
            }

            return BadRequest("Người dùng không có quyền thực hiện hành động này.");
        }
        [HttpGet("GetMessageThread")]
        public async Task<IActionResult> GetMessageThread(string senderId, string recipientId, int skip, int take)
        {
            if (skip < 0 || take <= 0)
            {
                return BadRequest("Tham số không hợp lệ.");
            }
            var messages = await _messageService.GetMessageThread(senderId, recipientId, skip, take);
            return Ok(messages);
        }
        [HttpGet("GetLastMessage")]
        public async Task<IActionResult> GetLastMessage(string senderId, string recipientId)
        {
            var message = await _messageService.GetLastMessageAsync(senderId, recipientId);
            return Ok(message);
        }

    }
}