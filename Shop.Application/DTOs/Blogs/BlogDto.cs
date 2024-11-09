namespace Shop.Application.DTOs.Blogs

{
    public class BlogDto : BlogBase
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
