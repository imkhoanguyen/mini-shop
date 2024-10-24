using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Data.Enum;
using API.DTOs;

namespace API.Entities
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