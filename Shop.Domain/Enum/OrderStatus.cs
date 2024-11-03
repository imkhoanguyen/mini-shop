namespace Shop.Domain.Enum
{
    public enum OrderStatus
    {
        Pending,
        PaymentReceived,
        PaymentFailed,
        PaymentMismatch,
        Refunded
    }
}
