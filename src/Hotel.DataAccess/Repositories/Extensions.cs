using Hotel.DataAccess.Entities;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Hotel.DataAccess.Repositories;

public static class Extensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // add normally
        services.AddScoped < IGenericRepository<RoomRegulation>, GenericRepository<RoomRegulation>>();
        services.AddScoped<IRoomRegulationRepository,RoomRegulationRepository>();
        return services;
    }
}
