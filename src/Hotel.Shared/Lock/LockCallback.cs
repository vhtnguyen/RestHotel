using org.apache.zookeeper.recipes.@lock;

namespace Hotel.Shared.Lock;

internal class LockCallback : LockListener
{
    private readonly Func<Task>? _lockAcquireHandler;
    private readonly Func<Task>? _lockReleaseHandler;

    public LockCallback(Func<Task>? lockAcquireHandler, Func<Task>? lockReleaseHandler)
    {
        _lockAcquireHandler = lockAcquireHandler;
        _lockReleaseHandler = lockReleaseHandler;
    }

    public Task lockAcquired()
    {
        if (_lockAcquireHandler != null)
        {
            return _lockAcquireHandler();
        }
        return Task.CompletedTask;
    }

    public Task lockReleased()
    {
        if(_lockReleaseHandler != null)
        {
            return _lockReleaseHandler();
        }    
        return Task.CompletedTask;
    }
}
