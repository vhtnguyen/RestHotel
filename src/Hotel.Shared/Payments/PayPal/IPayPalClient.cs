namespace Hotel.Shared.Payments.PayPal;

public interface IPayPalClient
{
    Task<SessionResource> CreateSession(CreateSessionResouce resource);
}
