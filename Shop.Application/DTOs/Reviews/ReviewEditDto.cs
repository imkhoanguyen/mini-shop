namespace Shop.Application.DTOs.Reviews
{
    public class ReviewEditDto()
    {
        public int Id { get; set; }
        public int? Rating { get; set; }
        public required string ReviewText { get; set; }
    }
}
