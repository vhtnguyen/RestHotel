namespace Hotel.Shared.Lock;

public interface IDistributedLockFactory
{
    IDistributedLocker Create(string lockName);
}
