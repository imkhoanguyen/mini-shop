namespace Shop.Domain.Entities
{
    public class Address : BaseEntity
    {
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Street { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public string? fullName { get; set; }
        public string? phone { get; set; }
    }
}