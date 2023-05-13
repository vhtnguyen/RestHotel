namespace Hotel.Shared.Payments.Stripe.Checkout;

public record CreateChargeResource(
    string Currency,
    long Amount,
    string CustomerId,
    string ReceiptEmail,
    string Description);
