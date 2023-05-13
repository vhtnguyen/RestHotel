namespace Hotel.Shared.Payments.Momo;

public interface IMomoClient
{
    Task<MomoResouce> CreateSession(CreateSessionResource resouce);
}
