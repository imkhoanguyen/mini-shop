namespace Shop.Application.DTOs.ShippingMethods
{
    public class ShippingMethodBase
    {
        public required string Name { get; set; }
        public required decimal Cost { get; set; }
        public string EstimatedDeliveryTime { get; set; }
    }
}