namespace Shop.Application.DTOs.Address
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Street { get; set; }
        public string? AppUserId { get; set; }
        // public AppUser? AppUser { get; set; }
    }
}
