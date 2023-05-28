using Hotel.DataAccess.Context;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Hotel.DataAccess;

public static class Extensions
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();
        // scan all implemented inteface
        services.Scan(scan =>
            scan.FromAssemblies(assembly)
            .AddClasses(c => c.Where(type => type.GetInterfaces().Any()))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );
        // scan repositories which extend Generic Repository
        // services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        // execute extension method addSql
        services.AddSql();
        return services;
    }
}
