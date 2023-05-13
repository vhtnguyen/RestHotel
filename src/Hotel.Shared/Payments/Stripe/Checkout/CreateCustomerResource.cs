namespace Hotel.Shared.Payments.Stripe.Checkout;

public record CreateCustomerResource(
    string Email,
    string Name,
    CreateCardResource Card);