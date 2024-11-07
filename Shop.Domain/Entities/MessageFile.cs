using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Domain.Entities
{
    public class MessageFile : BaseEntity
    {
        public string? publicId { get; set; }
        public string? FileUrl { get; set; }
        public string? FileType { get; set; }
        public int MessageId { get; set; }
        [ForeignKey("MessageId")]
        public Message? Message { get; set; }
    }
}
