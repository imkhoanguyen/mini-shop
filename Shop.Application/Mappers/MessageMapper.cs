using Shop.Application.DTOs.Messages;
using Shop.Domain.Entities;

namespace Shop.Application.Mappers
{
    public class MessageMapper
    {
        public static Message MessageDtoToEntity(MessageDto messageDto)
        {
            return new Message
            {
                Id = messageDto.Id,
                SenderId = messageDto.SenderId,
                RecipientId = messageDto.RecipientId,
                Content = messageDto.Content,
                FileUrl = messageDto.FileUrl,
                FileType = messageDto.FileType,
                SentAt = messageDto.SentAt,
            };
        }

        public static Message MessageAddDtoToEntity(MessageAdd messageAddDto)
        {
            return new Message
            {
                SenderId = messageAddDto.SenderId,
                RecipientId = messageAddDto.RecipientId,
                Content = messageAddDto.Content,
                FileUrl = messageAddDto.FileUrl,
                FileType = messageAddDto.FileType,
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
                FileUrl = message.FileUrl,
                FileType = message.FileType,
                SentAt = message.SentAt,
            };
        }
    }
}
