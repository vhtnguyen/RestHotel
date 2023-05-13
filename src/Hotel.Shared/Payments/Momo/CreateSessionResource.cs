namespace Hotel.Shared.Payments.Momo;

public record CreateSessionResource(
    string OrderId, 
    string RequestId,
    List<CreateSessionItemResouce> Items);