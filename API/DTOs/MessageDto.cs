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
        public List<string>? RecipientIds { get; set; }
        public string? Content { get; set; }
        public string? FileUrl { get; set; }
        public string? FileType { get; set; }
        public string? RepliedById { get; set; }
        public bool IsReplied { get; set; }
        public static Message ToMessageAsync(MessageAddDto messageAddDto)
        {


            return new Message
            {
                SenderId = messageAddDto.SenderId,
                RecipientIds = messageAddDto.RecipientIds,
                Content = messageAddDto.Content,
                FileUrl = messageAddDto.FileUrl,
                FileType = messageAddDto.FileType,
                SentAt = DateTime.UtcNow,
                RepliedById = messageAddDto.RepliedById,
                IsReplied = messageAddDto.IsReplied
            };
        }
    }

    public class MessageDto
    {
        public int Id { get; set; }
        public string? SenderId { get; set; }
        public List<string>? RecipientIds { get; set; }
        public string? Content { get; set; }
        public string? FileUrl { get; set; }
        public string? FileType { get; set; }
        public DateTime SentAt { get; set; }
        public string? RepliedById { get; set; }
        public bool IsReplied { get; set; }
        public static Message ToMessage(MessageDto messageDto)
        {
            return new Message
            {
                Id = messageDto.Id,
                SenderId = messageDto.SenderId,
                RecipientIds = messageDto.RecipientIds,
                Content = messageDto.Content,
                FileUrl = messageDto.FileUrl,
                FileType = messageDto.FileType,
                SentAt = messageDto.SentAt,
                RepliedById = messageDto.RepliedById,
                IsReplied = messageDto.IsReplied
            };
        }
    }
}