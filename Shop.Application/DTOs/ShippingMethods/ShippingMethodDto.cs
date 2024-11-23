namespace Shop.Application.DTOs.ShippingMethods
{
    public class ShippingMethodDto : ShippingMethodBase
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}