namespace Shop.Domain.Entities
{
    public class ShippingMethod : BaseEntity
    {
        public string Name { get; set; } = null!;
        public decimal Cost { get; set; }
        public string EstimatedDeliveryTime { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool IsDelete { get; set; } = false;
    }
}