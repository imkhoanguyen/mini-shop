using System.ComponentModel.DataAnnotations;
namespace Shop.Application.DTOs.AddLikedProduct{
    public class AddLikedProductDto
    {
        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public int ProductId { get; set; }
    }
}

