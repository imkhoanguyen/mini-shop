namespace Shop.Application.Parameters
{
    public class OrderParams : BaseParams
    {
        public string SelectedPaymentStatus { get; set; } = "";
        public string SelectedStatus { get; set; } = "";
        public string UserId { get; set; } = "";
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
