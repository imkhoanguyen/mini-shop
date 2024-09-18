using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace API.Entities
{
    public class Image : BaseEntity
    {
        [Required]
        [Url]
        public string Url { get; set; } = null!;
        public bool IsMain { get; set; } = false;
        public string? PublicId { get; set; }
        public bool IsApproved { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

        public string? AppUserId { get; set; }

        [ForeignKey("AppUserId")]
        public AppUser? AppUser { get; set; }

        public static ImageDto toImageDto(Image image){
            return new ImageDto
            {
                Id = image.Id,
                Url = image.Url,
                IsMain = image.IsMain,
            };
        }
    }
}