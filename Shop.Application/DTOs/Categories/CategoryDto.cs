namespace Shop.Application.DTOs.Categories
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}