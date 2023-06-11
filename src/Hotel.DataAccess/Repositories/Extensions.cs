using Hotel.DataAccess.Entities;
using Hotel.DataAccess.Repositories.IRepositories;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Hotel.DataAccess.Repositories;

public static class Extensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // add normally
        services.AddScoped< IGenericRepository<RoomRegulation>, GenericRepository<RoomRegulation>>();
        services.AddScoped<IRoomRegulationRepository,RoomRegulationRepository>();
        services.AddScoped<IInvoiceRepository,InvoiceRepository>();
        services.AddScoped<IGenericRepository<Invoice>, GenericRepository<Invoice>>();
        services.AddScoped<IReservationRepository,ReservationRepository>();
        return services;
    }
}
