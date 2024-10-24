namespace API.DTOs
{
    public class ImageApprovalDto
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public string? UserName { get; set; }
        public int VariantId { get; set; }
        public bool IsApproved { get; set; }
    }
}