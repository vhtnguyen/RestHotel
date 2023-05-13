namespace Hotel.Shared.Payments.Stripe.Checkout;
public record CustomerResource(
    string CustomerId,
    string Email,
    string Name);
