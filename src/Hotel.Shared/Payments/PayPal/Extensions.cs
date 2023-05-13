using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.Shared.Payments.PayPal;

public static class Extensions
{
    public static IServiceCollection AddPayPayCheckout(this IServiceCollection services)
    {
        using var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetService<IConfiguration>()!;

        // bind options
        var section = configuration.GetSection("paypal");
        var options = new PayPalOptions();
        section.Bind(options);
        services.Configure<PayPalOptions>(section);

        // inject
        services.AddScoped<IPayPalApiContext, PayPalApiContext>();
        services.AddScoped<IPayPalClient, PayPalClient>();

        return services;
    }
}
