using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Domain.Entities
{
    public class ProductImage : BaseEntity
    {
        public string? PublicId { get; set; }
        public string? ImgUrl { get; set; }
        // nav
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
    }
}
