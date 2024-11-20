using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Shop.Application.DTOs.Messages;
using Shop.Application.Mappers;
using Shop.Application.Repositories;
using Shop.Application.Services.Abstracts;
using Shop.Domain.Entities;
using Shop.Domain.Exceptions;

namespace Shop.Application.Services.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unit;
        private readonly RoleManager<AppRole> _role;
        private readonly UserManager<AppUser> _user;
        public MessageService(IUnitOfWork unit, RoleManager<AppRole> role, UserManager<AppUser> user)
        {
            _unit = unit;
            _role = role;
            _user = user;
        }
        public async Task<List<string>> GetUsersByClaimValueAsync(string claimValue)
        {
            var rolesWithClaim = await _unit.MessageRepository.GetRoleWithClaim(claimValue);

            var userIds = new List<string>();

            foreach (var roleId in rolesWithClaim)
            {
                var role = await _role.FindByIdAsync(roleId.ToString());
                if (role != null)
                {
                    var usersInRole = await _user.GetUsersInRoleAsync(role.Name!);
                    userIds.AddRange(usersInRole.Select(u => u.Id));
                }
            }
            return userIds.Distinct().ToList();
        }
        public async Task<MessageDto> AddMessageAsync(MessageAdd messageAdd)
        {
            var lastMessage = await _unit.MessageRepository.GetLastMessageAsync(messageAdd.SenderId!, messageAdd.RecipientIds!.FirstOrDefault()!);
            if (lastMessage != null && lastMessage.IsReplied && lastMessage.RepliedById != messageAdd.SenderId)
            {
                messageAdd.RecipientIds = new List<string> { lastMessage.RepliedById! };
            }

            var message = MessageMapper.MessageAddDtoToEntity(messageAdd);
            await _unit.MessageRepository.AddAsync(message);

            return await _unit.CompleteAsync()
                ? MessageMapper.EntityToMessageDto(message)
                : throw new BadRequestException("Thêm tin nhắn thất bại");
        }
        public async Task<MessageDto> ReplyMessageAsync(MessageAdd messageAdd)
        {
            var lastMessage = await _unit.MessageRepository.GetLastMessageAsync(messageAdd.SenderId!, messageAdd.RecipientIds!.FirstOrDefault()!);
            var message = MessageMapper.MessageAddDtoToEntity(messageAdd);

            if (lastMessage != null && lastMessage.IsReplied)
            {
                if (lastMessage.RepliedById != messageAdd.SenderId)
                {
                    throw new BadRequestException("Người dùng không có quyền phản hồi tin nhắn này.");
                }
            }
            else
            {
                message.IsReplied = true;
                message.RepliedById = message.SenderId;
            }
            await _unit.MessageRepository.AddAsync(message);

            return await _unit.CompleteAsync()
                ? MessageMapper.EntityToMessageDto(message)
                : throw new BadRequestException("Thêm tin nhắn thất bại");

        }
        public Task AddFileAsync(IFormFileCollection files)
        {
            throw new NotImplementedException();
        }

        public async Task<MessageDto> GetLastMessageAsync(string senderId, string recipientId)
        {
            var lastMessage = await _unit.MessageRepository.GetLastMessageAsync(senderId, recipientId);
            return MessageMapper.EntityToMessageDto(lastMessage);
        }

        public async Task<IEnumerable<MessageDto>> GetMessageThread(string senderId, string recipientId, int skip, int take)
        {
            var messages = await _unit.MessageRepository.GetMessageThread(senderId, recipientId, skip, take);
            return messages.Select(MessageMapper.EntityToMessageDto!);
        }

        
    }
}
