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
        public AppUser? AppUser { get; set; }
        public string? UserId { get; set; }
    }
}
