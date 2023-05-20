
using org.apache.zookeeper;

namespace Hotel.Shared.Lock;

internal class DistributedLockFactory : IDistributedLockFactory
{
    private readonly ZooKeeper _zooKeeper;
    public DistributedLockFactory(ZooKeeper zooKeeper)
    {
        _zooKeeper = zooKeeper;
    }
    public IDistributedLocker Create(string lockName)
    {
        return new DistributedLocker($"/{lockName}", _zooKeeper);
    }
}
