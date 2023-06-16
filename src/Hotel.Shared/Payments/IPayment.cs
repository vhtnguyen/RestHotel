namespace Hotel.Shared.Payments;

public interface IPayment
{
    Task<SessionResource> CreateSession(CreateSessionResource resource);
}