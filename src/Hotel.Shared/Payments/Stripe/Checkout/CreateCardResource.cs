namespace Hotel.Shared.Payments.Stripe.Checkout;
public record CreateCardResource(
    string Name,
    string Number,
    string ExpiryYear,
    string ExpiryMonth,
    string Cvc);
