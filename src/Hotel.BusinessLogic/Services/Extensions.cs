
using Hotel.BusinessLogic.Services.IServices;
using Microsoft.Extensions.DependencyInjection;
namespace Hotel.BusinessLogic.Services
{

    public static class Extensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // add normally\
            services.AddScoped<IInvoiceService, InvoiceService>();
            return services;
        }
    }
}
