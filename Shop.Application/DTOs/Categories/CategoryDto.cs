namespace Shop.Application.DTOs.Categories
{
    public class CategoryDto : CategoryBase
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
