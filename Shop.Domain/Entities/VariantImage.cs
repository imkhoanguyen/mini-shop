using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Domain.Entities
{
    public class VariantImage : BaseEntity
    {
        public string? PublicId { get; set; }
        public string? ImgUrl { get; set; }
        // nav
        public int VariantId { get; set; }
        [ForeignKey("VariantId")]
        public Variant? Variant { get; set; }
    }
}
