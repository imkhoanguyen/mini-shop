using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class Payments : BaseEntity
    {
        [Required]
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order? Order { get; set; }
        public string Payment_method { get; set; }
        public int Status { get; set; }
        public DateTime Payment_date { get; set; }

    }
}