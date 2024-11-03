using Shop.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Shop.Application.DTOs.Messages
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
        
    }
}
