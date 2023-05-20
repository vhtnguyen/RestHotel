using org.apache.zookeeper;

namespace Hotel.Shared.Lock;

public class DistributedLocker : IDistributedLocker
{
    private readonly string _lockName;
    private readonly ZooKeeper _zooKeeper;

    public DistributedLocker(string lockName, ZooKeeper zooKeeper)
    {
        _lockName = lockName;
        _zooKeeper = zooKeeper;
    }
    public IDistributedLock GetLocker()
    {
        return new DistributedLock(_lockName, _zooKeeper);
    }
}
