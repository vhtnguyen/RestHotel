using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Hotel.Shared.Redis;

public static class Extensions
{
    public static IStreamingSubscriber UseRedisStreaming(this WebApplication application)
    {
        return new StreamingSubscriber(application);
    }

    public static IServiceCollection AddRedis(this IServiceCollection services)
    {
        // get configuration
        using var provider = services.BuildServiceProvider();
        var configuration = provider.GetService<IConfiguration>()!;

        // bind section
        var options = new RedisOptions();
        var section = configuration.GetSection("redis");
        section.Bind(options);

        // inject options
        services.Configure<RedisOptions>(section);

        var connection = ConnectionMultiplexer.Connect(options.ConnectionString);
        services.AddSingleton<IConnectionMultiplexer>(connection);

        // add cache service
        services.AddSingleton<ICacheService, CacheService>();
        // add streaming service
        services.AddSingleton<IStreamingPublisher, StreamingPublisher>();
        //services.AddSingleton<IStreamingSubscriber, StreamingSubscriber>();
        return services;
    }
}
