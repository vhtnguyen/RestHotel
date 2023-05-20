
namespace Hotel.Shared.Lock;

public interface IDistributedLocker
{
    IDistributedLock GetLocker();
}
