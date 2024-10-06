using API.Entities;

namespace API.DTOs
{
    public class BlogsDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Category { get; set; }
        public DateTime Create { get; set; } = DateTime.Now;
        public DateTime Update { get; set; }
        // nav
        public AppUser? AppUser { get; set; }
        public string? UserId { get; set; }

        public static Blog toBlog(BlogsDto blogDTO)
        {
           return new Blog { Id =blogDTO.Id,Title = blogDTO.Title, Content = blogDTO.Content, Category = blogDTO.Category, Create = blogDTO.Create,Update=blogDTO.Update };  
        }

    }
}
