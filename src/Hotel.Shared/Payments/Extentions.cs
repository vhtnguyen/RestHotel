using Hotel.Shared.Payments.Momo;
using Hotel.Shared.Payments.PayPal;
using Hotel.Shared.Payments.Stripe;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.Shared.Payments;

public static class Extensions
{
    public static IServiceCollection AddPayment(this IServiceCollection services)
    {
        // get configuration
        using var provider = services.BuildServiceProvider();
        var configuration = provider.GetService<IConfiguration>()!;

        // bind section
        var options = new PaymentOptions();
        var section = configuration.GetSection("payment");
        section.Bind(options);

        // inject options
        services.Configure<PaymentOptions>(section);

        services.AddStripeCheckout();
        services.AddPayPalCheckout();
        services.AddMomoCheckout();
        services.AddScoped<IPaymentFactory, PaymentFactory>();
        return services;
    }
}