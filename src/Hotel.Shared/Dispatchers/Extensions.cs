using Hotel.Shared.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Hotel.Shared.Dispatchers;

public static class Extensions
{
    public static IServiceCollection AddDispatcher(this IServiceCollection services)
    {
        // scan all of the command had implemented the command handler
        var assembly = Assembly.GetCallingAssembly();
        services.Scan(scan =>
            scan.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        // add dispatcher
        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
        return services;
    }
}
