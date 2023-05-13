namespace Hotel.Shared.Payments.Stripe.Link;

public record CreateSessionItemResource(
    string Name, decimal Price, int Quantity);
