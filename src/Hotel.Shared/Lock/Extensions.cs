
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using org.apache.zookeeper;
using System.Diagnostics;

namespace Hotel.Shared.Lock;

public static class Extensions
{
    public static IServiceCollection AddDistributedLock(this IServiceCollection services)
    {
        using var serviceProvider  = services.BuildServiceProvider();
        var configuration = serviceProvider.GetService<IConfiguration>()!;

        //bind section
        var section = configuration.GetSection("zooKeeper");
        var options = new ZooKeeperOptions();
        section.Bind(options);
        services.Configure<ZooKeeperOptions>(section);

        var zooKeeper = createConnection(options);
        services.AddSingleton<ZooKeeper>(zooKeeper);
        services.AddSingleton<IDistributedLockFactory, DistributedLockFactory>();

        return services;
    }

    private static ZooKeeper createConnection(ZooKeeperOptions options)
    {
        var zooKeeper = new ZooKeeper(options.Host, options.ConnectionTimeout, NullWatcher.Instance);
        var sw = new Stopwatch();

        sw.Start();
        while(sw.ElapsedMilliseconds < options.ConnectionTimeout)
        {
            var state = zooKeeper.getState();
            if(state == ZooKeeper.States.NOT_CONNECTED || state == ZooKeeper.States.CONNECTING)
            {
                break;
            }
        }

        sw.Stop();

        return zooKeeper;
    }
}

class NullWatcher : Watcher
{
    public static readonly NullWatcher Instance = new NullWatcher();
    public override Task process(WatchedEvent @event)
    {
        return Task.CompletedTask;
    }
}
