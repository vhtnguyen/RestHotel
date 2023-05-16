namespace Hotel.Shared.Payments.Momo;

public interface IMomoClient
{
    Task<SessionResource> CreateSession(CreateSessionResource resouce);
}
