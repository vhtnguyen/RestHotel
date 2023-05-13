using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoMo;

namespace Hotel.Shared.Payments.Momo;

/*
    send post request to momo endpoint 
*/
public static class Extensions
{
    public static IServiceCollection AddMomoCheckoutLink(this IServiceCollection services)
    {
        using var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetService<IConfiguration>()!;

        var section = configuration.GetSection("momo");
        var options = new MomoOptions();
        section.Bind(options);
        services.Configure<MomoOptions>(section);

        // add service
        services.AddScoped<IMomoPaymentRequest, MomoPaymentRequest>();
        services.AddScoped<IMomoSecurity, MoMoSecurity>();
        services.AddScoped<IMomoClient, MomoClient>();

        return services;
    }
}

