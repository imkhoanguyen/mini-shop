using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Shop.Application.DTOs.Categories;
using Shop.Application.DTOs.Messages;
using Shop.Application.Mappers;
using Shop.Application.Parameters;
using Shop.Application.Repositories;
using Shop.Application.Services.Abstracts;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;
using Shop.Domain.Exceptions;

namespace Shop.Application.Services.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unit;
        private readonly RoleManager<AppRole> _role;
        private readonly UserManager<AppUser> _user;
        private readonly ICloudinaryService _cloudinaryService;
         
        public MessageService(IUnitOfWork unit, RoleManager<AppRole> role, UserManager<AppUser> user, ICloudinaryService cloudinaryService)
        {
            _unit = unit;
            _role = role;
            _user = user;
            _cloudinaryService = cloudinaryService;
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

        public async Task<List<string>> GetUsersWithoutClaimAsync(string claimValue)
        {
            var rolesWithClaim = await _unit.MessageRepository.GetRoleWithClaim(claimValue);

            var allUserIds = _user.Users.Select(u => u.Id).ToList();

            var adminUserIds = new List<string>();

            foreach (var roleId in rolesWithClaim)
            {
                var role = await _role.FindByIdAsync(roleId.ToString());
                if (role != null)
                {
                    var usersInRole = await _user.GetUsersInRoleAsync(role.Name!);
                    adminUserIds.AddRange(usersInRole.Select(u => u.Id));
                }
            }

            var customerUserIds = allUserIds.Except(adminUserIds).ToList();
            return customerUserIds;
        }


        public async Task<MessageDto> AddMessageAsync(MessageAdd messageAdd, string claimValue)
        {
            var message = MessageMapper.MessageAddDtoToEntity(messageAdd);

            var adminIds = await GetUsersByClaimValueAsync(claimValue); 
            message.RecipientIds = adminIds;
            
           
            if (messageAdd.Files?.Count > 0)
            {
                foreach (var file in messageAdd.Files)
                {
                    var uploadResult = await _cloudinaryService.UploadFileAsync(file);
                    if (uploadResult.Error != null)
                    {
                        throw new BadRequestException("Lỗi khi thêm file");
                    }
                    var messageFile = new MessageFile
                    {
                        FileUrl = uploadResult.Url,
                        FileType = file.ContentType,
                    };
                    message.Files.Add(messageFile);
                }
            }

            await _unit.MessageRepository.AddAsync(message);

            return await _unit.CompleteAsync()
                ? MessageMapper.EntityToMessageDto(message)
                : throw new BadRequestException("Thêm tin nhắn thất bại");
        }
        public async Task<MessageDto> ReplyMessageAsync(MessageAdd messageAdd, string claimValue)
        {
            var message = MessageMapper.MessageAddDtoToEntity(messageAdd);

            
            var customerId = messageAdd.RecipientIds;
            var otherAdmins = await GetUsersByClaimValueAsync(claimValue);
            otherAdmins.Remove(messageAdd.SenderId!);

            message.RecipientIds = customerId.Concat(otherAdmins).ToList();
   
            if (messageAdd.Files?.Count > 0)
            {
                foreach (var file in messageAdd.Files)
                {
                    var uploadResult = await _cloudinaryService.UploadFileAsync(file);
                    if (uploadResult.Error != null)
                    {
                        throw new BadRequestException("Lỗi khi thêm file");
                    }
                    var messageFile = new MessageFile
                    {
                        FileUrl = uploadResult.Url,
                        FileType = file.ContentType,
                    };
                    message.Files.Add(messageFile);
                }
            }

            message.RepliedByAdminId = message.SenderId;

            await _unit.MessageRepository.AddAsync(message);

            return await _unit.CompleteAsync()
                ? MessageMapper.EntityToMessageDto(message)
                : throw new BadRequestException("Thêm tin nhắn thất bại");

        }

        public async Task<MessageDto> GetLastMessageAsync(string userId)
        {
            var lastMessage = await _unit.MessageRepository.GetLastMessageAsync(userId);
            if (lastMessage == null) return null;
            return MessageMapper.EntityToMessageDto(lastMessage);
        }

        public async Task<PagedList<MessageDto>> GetMessageThread(MessageParams messageParams, string customerId)
        {
            var messages = await _unit.MessageRepository.GetMessageThread(messageParams, customerId);
            var messageDtos = messages.Select(MessageMapper.EntityToMessageDto!);
            return new PagedList<MessageDto>(messageDtos, messages.TotalCount, messageParams.PageNumber, messageParams.PageSize);

        }

        
    }
}
