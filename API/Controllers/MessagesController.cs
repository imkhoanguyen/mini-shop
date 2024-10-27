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
        private readonly ICloudinaryService _fileService;
        public MessagesController(IMessageRepository message, IUnitOfWork unitOfWork, IHubContext<ChatHub> hub, ICloudinaryService fileService)
        {
            _message = message;
            _unitOfWork = unitOfWork;
            _hub = hub;
            _fileService = fileService;
        }
        [HttpPost("UploadFiles")]
        public async Task<IActionResult> UploadFiles([FromForm] IFormFileCollection files)
        {
            // Nếu không có tệp, trả về response trống
            if (files == null || files.Count == 0)
            {
                return Ok(new { files = new List<object>() });
            }

            var uploadResults = new List<object>();

            foreach (var file in files)
            {
                var uploadResult = await _fileService.UploadFileAsync(file);
                if (uploadResult == null)
                {
                    return BadRequest(new { message = $"File '{file.FileName}' upload failed." });
                }

                uploadResults.Add(new
                {
                    fileUrl = uploadResult.Url.ToString(),
                    fileType = file.ContentType
                });
            }

            return Ok(new { files = uploadResults });
        }

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] MessageAddDto messageDto)
        {

            var message = MessageAddDto.ToMessageAsync(messageDto);
            await _message.SendMessage(message);
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
        [HttpPost("ReplyMessage")]
        public async Task<IActionResult> ReplyMessage(int messageId, string repliedById)
        {

            if (await _message.ReplyMessage(messageId, repliedById))
            {
                if (await _unitOfWork.Complete())
                {
                    return Ok(new { message = "Message replied successfully" });
                }
                return BadRequest(new { message = "Message replied failed." });
            }
            return BadRequest(new { message = "Message replied failed." });
        }
        [HttpGet("GetMessagesForEmployee")]
        public async Task<IActionResult> GetMessagesForEmployee(string employeeId)
        {
            var messages = await _message.GetMessagesForEmployee(employeeId);
            return Ok(messages);
        }
        [HttpGet("GetMessageById")]
        public async Task<IActionResult> GetMessageById(int messageId)
        {
            var message = await _message.GetMessageById(messageId);
            return Ok(message);
        }
    }
}