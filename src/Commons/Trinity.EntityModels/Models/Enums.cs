namespace Trinity.EntityModels
{
    public enum UserRole
    {
        Customer,
        Admin,
        Manager
    }

    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Canceled
    }

    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed,
        Refunded
    }

    public enum PaymentMethod
    {
        CreditCard,
        PayPal,
        BankTransfer,
        CashOnDelivery
    }
}
