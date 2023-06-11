using Hotel.BusinessLogic.Handlers;
using Hotel.BusinessLogic.Profiles;
using Hotel.BusinessLogic.Services;
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
        services.AddScoped<IInvoiceService, InvoiceService>();

        services.AddMapper();
        return services;
    }
}
