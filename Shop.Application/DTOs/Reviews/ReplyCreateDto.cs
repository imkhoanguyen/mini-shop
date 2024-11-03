namespace Shop.Application.DTOs.Reviews
{
    public class ReplyCreateDto()
    {
        public required string ReviewText { get; set; }
        public int ParentReviewId { get; set; }
        public required string UserId { get; set; }
        public int ProductId { get; set; }
    }
}
