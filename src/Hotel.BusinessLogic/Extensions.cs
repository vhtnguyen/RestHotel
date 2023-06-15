using Hotel.BusinessLogic.Handlers;
using Hotel.BusinessLogic.Profiles;
using Hotel.BusinessLogic.Services;
using Hotel.BusinessLogic.Services.IServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic;

public static class Extensions
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        services.AddHandlers();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IHotelServicesService, HotelServicesService>();
        services.AddScoped<IReservationService, ReservationService>();
        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddScoped<IReservationCancellationService, ReservationCancellationService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IRoomRegulationService, RoomRegulationService>();
        // services.AddScoped<IRoomService, RoomService>();


        services.AddMapper();
        return services;
    }
}
