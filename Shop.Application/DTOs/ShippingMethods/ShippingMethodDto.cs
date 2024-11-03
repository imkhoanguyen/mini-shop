namespace Shop.Application.DTOs.ShippingMethods
{
    public class ShippingMethodDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public DateTime EstimatedDeliveryTime { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}