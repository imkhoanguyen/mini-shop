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
        public MessageService(IUnitOfWork unit, RoleManager<AppRole> role)
        {
            _unit = unit;
            _role = role;
        }
        public async Task<IEnumerable<AppUser>> GetReplyUserId()
        {

        }
        public async Task<MessageDto> AddMessageAsync(MessageAdd messageAdd)
        {
            var message = MessageMapper.MessageAddDtoToEntity(messageAdd);
            await _unit.MessageRepository.AddAsync(message);

            return await _unit.CompleteAsync()
                ? MessageMapper.EntityToMessageDto(message)
                : throw new BadRequestException("Thêm tin nhắn thất bại");
        }

        public async Task<MessageDto> GetLastMessage(string senderId, string recipientId)
        {
            var message = await _unit.MessageRepository.GetLastMessage(senderId, recipientId);
            return MessageMapper.EntityToMessageDto(message);
        }

        public async Task<IEnumerable<MessageDto>> GetMessageThread(string senderId, string recipientId, int skip, int take)
        {
            var messages = await _unit.MessageRepository.GetMessageThread(senderId, recipientId, skip, take);
            return messages.Select(MessageMapper.EntityToMessageDto!);
        }
        public async Task AddFileAsync(IFormFileCollection files)
        {

        }
    }
}
