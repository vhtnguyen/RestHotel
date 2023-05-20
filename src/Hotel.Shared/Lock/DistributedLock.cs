using org.apache.zookeeper;
using org.apache.zookeeper.recipes.@lock;

namespace Hotel.Shared.Lock;

internal class DistributedLock : IDistributedLock
{
    private readonly WriteLock _writeLock;
    public DistributedLock(string path, ZooKeeper zooKeeper)
    {
        _writeLock = new WriteLock(zooKeeper, path, null);
    }
    public Task LockAsync(Func<Task> acquireLock, Func<Task>? releaseLock = null)
    {
        var lockCallback = new LockCallback(async() =>
        {
            await acquireLock();
            await _writeLock.unlock();
            
        }, releaseLock);

        _writeLock.setLockListener(lockCallback);
        return _writeLock.Lock();
    }
}
