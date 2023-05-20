using Hotel.Shared.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Hotel.Shared.Dispatchers;

public static class Extensions
{
    public static IServiceCollection AddDispatcher(this IServiceCollection services)
    {
        // add dispatcher
        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
        return services;
    }
}
