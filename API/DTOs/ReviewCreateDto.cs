namespace API.DTOs
{
    public class ReviewCreateDto
    {
        public required string ReviewText { get; set; }
        public int? Rating { get; set; }

    }
}
