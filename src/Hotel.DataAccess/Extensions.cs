using Hotel.DataAccess.Context;
using Hotel.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.DataAccess;

public static class Extensions
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
    {
        // scan repositories which extend Generic Repository
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        // execute extension method addSql
        services.AddSql();
        return services;
    }
}
