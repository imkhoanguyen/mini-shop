using System.ComponentModel.DataAnnotations;
using API.Entities;
using API.Interfaces;

namespace API.DTOs
{
    public class MessageAddDto
    {
        [Required]
        public string SenderId { get; set; } = null!;
        [Required]
        public string RecipientId { get; set; } = null!;
        public string? Content { get; set; }
        public string? FileUrl { get; set; }
        public string? FileType { get; set; }
        public static async Task<Message> ToMessageAsync(MessageAddDto messageAddDto)
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
    }

    public class MessageDto
    {
        public int Id { get; set; }

        public string? SenderId { get; set; }

        public string? RecipientId { get; set; }

        public string? Content { get; set; }
        
        public string? FileUrl { get; set; }
        
        public string? FileType { get; set; }
        
        public DateTime SentAt { get; set; }

        public static Message ToMessage(MessageDto messageDto)
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
    }
}