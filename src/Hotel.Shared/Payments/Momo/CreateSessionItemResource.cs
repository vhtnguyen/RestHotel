namespace Hotel.Shared.Payments.Momo;

public record CreateSessionItemResouce(
    string Name,
    decimal Price,
    int Quantity);
