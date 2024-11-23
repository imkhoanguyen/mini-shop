namespace Shop.Domain.Entities{
    public class Payment : BaseEntity
    {
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public int Status { get; set; }
    }
}