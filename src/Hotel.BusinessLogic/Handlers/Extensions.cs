using Hotel.Shared.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.Handlers;

public static class Extensions
{
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        // scan all of the command had implemented the command handler
        var assembly = Assembly.GetCallingAssembly();
        services.Scan(scan =>
            scan.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }    
}
