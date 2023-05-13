namespace Hotel.Shared.Payments.PayPal;

public record CreateSessionResouce(
    string Currency,
    string RequestId, // dùng để kiểm tra xem success url trả về có đúng cùng 1 requestId hay không
    List<CreateSessionItemResouce> Items);
