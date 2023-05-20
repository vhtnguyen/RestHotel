namespace Hotel.Shared.Lock;

public interface IDistributedLock
{
    Task LockAsync(Func<Task> acquireLock, Func<Task>? releaseLock = null);
}
    