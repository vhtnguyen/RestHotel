namespace Hotel.Shared.Payments.Stripe.Link;

public record CreateSessionResource(
    string Currency,
    string RequestId,
    List<CreateSessionItemResource> Items);