using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Shop.Application.DTOs.Categories;
using Shop.Application.DTOs.Messages;
using Shop.Application.DTOs.Users;
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
        public async Task<List<UserDto>> GetUsersByClaimValueAsync(string claimValue)
        {
            var rolesWithClaim = await _unit.MessageRepository.GetRoleWithClaim(claimValue);

            var userList = new List<AppUser>();

            foreach (var roleId in rolesWithClaim)
            {
                var role = await _role.FindByIdAsync(roleId.ToString());
                if (role != null)
                {
                    var usersInRole = await _user.GetUsersInRoleAsync(role.Name);
                    userList.AddRange(usersInRole);
                }
            }
            return userList.Select(UserMapper.EntityToUserDto).ToList();
        }
        public async Task<MessageDto> AddMessageAsync(MessageAdd messageAdd)
        {
            var message = MessageMapper.MessageAddDtoToEntity(messageAdd);
            await _unit.MessageRepository.AddAsync(message);

            return await _unit.CompleteAsync()
                ? MessageMapper.EntityToMessageDto(message)
                : throw new BadRequestException("Thêm tin nhắn thất bại");
        }
        public async Task<MessageDto> ReplyMessageAsync(MessageAdd messageAdd)
        {
            var message = MessageMapper.MessageAddDtoToEntity(messageAdd);
            await _unit.MessageRepository.AddAsync(message);

            return await _unit.CompleteAsync()
                ? MessageMapper.EntityToMessageDto(message)
                : throw new BadRequestException("Thêm tin nhắn thất bại");
        }
        public Task AddFileAsync(IFormFileCollection files)
        {
            throw new NotImplementedException();
        }

        public Task<MessageDto> GetLastMessage(string senderId, string recipientId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MessageDto>> GetMessageThread(string senderId, string recipientId, int skip, int take)
        {
            throw new NotImplementedException();
        }

        
    }
}
