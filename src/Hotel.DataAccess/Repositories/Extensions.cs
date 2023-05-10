using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Hotel.DataAccess.Repositories;

public static class Extensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();
        // scan all implemented inteface
        services.Scan(scan => 
            scan.FromAssemblies(assembly)
            .AddClasses(c => c.Where(type => type.GetInterfaces().Any()))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );

        // add normally
        return services;
    }
}
