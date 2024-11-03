using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Domain.Entities
{
    public class Product_Voucher
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int VoucherId { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

        [ForeignKey("VoucherId")]
        public Voucher? Voucher { get; set; }
    }
}