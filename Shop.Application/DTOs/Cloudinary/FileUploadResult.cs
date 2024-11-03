namespace Shop.Application.DTOs.Cloudinary
{
    public class FileUploadResult
    {
        public string? PublicId { get; set; }
        public string? Url { get; set; }
        public string? Error { get; set; }
    }
}
