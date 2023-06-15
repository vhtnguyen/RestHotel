namespace Hotel.Shared.Payments;

public record CreateSessionResource(
    string OrderId,
    string RequestId,
    string? Currency,
    List<CreateSessionItemResouce> Items);