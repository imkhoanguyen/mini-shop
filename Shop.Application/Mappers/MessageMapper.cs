using Shop.Application.DTOs.Messages;
using Shop.Application.DTOs.Variants;
using Shop.Domain.Entities;

namespace Shop.Application.Mappers
{
    public class MessageMapper
    {

        public static Message MessageAddDtoToEntity(MessageAdd messageAddDto)
        {
            return new Message
            {
                SenderId = messageAddDto.SenderId,
                RecipientId = messageAddDto.RecipientId,
                Content = messageAddDto.Content,
                SentAt = DateTime.UtcNow
            };
        }

        public static MessageDto EntityToMessageDto(Message message)
        {
            return new MessageDto
            {
                Id = message.Id,
                SenderId = message.SenderId,
                RecipientId = message.RecipientId,
                Content = message.Content,
                SentAt = message.SentAt,
                Files = message.Files.Select(MessageFileToFileMessageDto).ToList()
            };
        }
        public static FileMessageDto MessageFileToFileMessageDto(MessageFile entity)
        {
            return new FileMessageDto
            {
                Id = entity.Id,
                FileUrl = entity.FileUrl ?? string.Empty,
                FileType = entity.FileType ?? string.Empty
            };
        }
    }
}
