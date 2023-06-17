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
        services.AddScoped<IGenericRepository<RoomRegulation>, GenericRepository<RoomRegulation>>();
        services.AddScoped<IRoomDetailRepository, RoomDetailRepository>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IRoomRegulationRepository, RoomRegulationRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IGenericRepository<Invoice>, GenericRepository<Invoice>>();
        services.AddScoped<IGenericRepository<Room>, GenericRepository<Room>>();
        services.AddScoped<IGenericRepository<ReservationCard>, GenericRepository<ReservationCard>>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<IInvoiceHotelServiceRepository, InvoiceHotelServiceRepository>();
        services.AddScoped<IRoomRevenueRepository, RoomRevenuRepository>();
        services.AddScoped<IRoomOccupancyRepository, RoomOccupancyRepository>();    
        return services;
    }
}
