using Microsoft.Extensions.DependencyInjection;

namespace Hotel.Shared.Messaging;

public static class Extensions
{
    public static IServiceCollection AddMessaging(this IServiceCollection services)
    {
        services.AddSingleton(typeof(IMessagingChannel<>), typeof(MessagingChannel<>));
        return services;
    }
}
