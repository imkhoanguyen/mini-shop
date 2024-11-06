using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using API.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Shop.Application.Services.Abstracts;

namespace API.Controllers
{
    public class MessagesController : BaseApiController
    {
        //private readonly IMessageRepository _message;
        //private readonly IUnitOfWork _unitOfWork;
        //private readonly IHubContext<ChatHub> _hub;
        //private readonly ICloudinaryService _fileService;
        //public MessagesController(IMessageRepository message, IUnitOfWork unitOfWork, IHubContext<ChatHub> hub, ICloudinaryService fileService)
        //{
        //    _message = message;
        //    _unitOfWork = unitOfWork;
        //    _hub = hub;
        //    _fileService = fileService;
        //}
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

        //[HttpPost("SendMessage")]
        //public async Task<IActionResult> SendMessage([FromBody] MessageAddDto messageDto)
        //{

        //    var message = await MessageAddDto.ToMessageAsync(messageDto);
        //    _message.AddMessage(message);
        //    if (await _unitOfWork.Complete())
        //    {
        //        return Ok(new { message = "Message sent successfully" });
        //    }
        //    return BadRequest(new { message = "Message sent failed." });
        //}

        //[HttpGet("GetMessageThread")]
        //public async Task<IActionResult> GetMessageThread(string senderId, string recipientId, int skip, int take)
        //{
        //    if (skip < 0 || take <= 0)
        //    {
        //        return BadRequest("Invalid skip or take parameters.");
        //    }
        //    var messages = await _message.GetMessageThread(senderId, recipientId, skip, take);
        //    return Ok(messages);
        //}
        //[HttpGet("GetLastMessage")]
        //public async Task<IActionResult> GetLastMessage(string senderId, string recipientId)
        //{
        //    var message = await _message.GetLastMessage(senderId, recipientId);
        //    return Ok(message);
        //}

    }
}