using API.DTOs;
using System.Reflection;

namespace API.Entities
{
    public class Blog : BaseEntity
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Category { get; set; }
        public DateTime Create { get; set; } = DateTime.Now;
        public DateTime Update { get; set; }
        // nav
        public AppUser? AppUser { get;  set; }
        public string? UserId { get; set; }





       public static BlogsDto toBlogDto (Blog blog)
        {
            return new BlogsDto
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                Category = blog.Category,
                Update = blog.Update,
                AppUser = blog.AppUser,
                UserId = blog.UserId,
            };
        }

    }
}
