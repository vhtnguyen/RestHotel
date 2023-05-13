namespace Hotel.Shared.Payments.PayPal;

public record CreateSessionItemResouce(
    string Name,
    decimal Price,
    int Quantity);
