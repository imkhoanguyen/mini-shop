using System.ComponentModel.DataAnnotations;
using API.DTOs;
using API.Helpers;

namespace API.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        public string Name { get; set; } = null!;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;
        public bool IsDelete { get; set; } = false;
        public ICollection<ProductCategory>? ProductCategories { get; set; } = new List<ProductCategory>();

        public static CategoryDto toCategoryDto(Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Created = category.Created,
                Updated = category.Updated,
            };
        }
        public static PageListDto<CategoryDto> toPageListDto(PageList<Category> pageList)
        {
            return new PageListDto<CategoryDto>
            {
                PageIndex = pageList.PageIndex,
                PageSize = pageList.PageSize,
                TotalCount = pageList.TotalCount,
                TotalPages = pageList.TotalPages,
                Items = pageList.Select(c => toCategoryDto(c)).ToList() // Chuyển đổi danh sách Category sang CategoryDto
            };
        }

       

    }


}