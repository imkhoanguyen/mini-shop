using System.ComponentModel.DataAnnotations;

namespace Shop.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
