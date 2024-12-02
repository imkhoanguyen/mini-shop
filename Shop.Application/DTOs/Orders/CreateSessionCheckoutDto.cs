namespace Shop.Application.DTOs.Orders
{
    public class CreateSessionCheckoutDto
    {
        public required OrderAddDto Order { get; set; }
        public required string CartId { get; set; }
    }
}
