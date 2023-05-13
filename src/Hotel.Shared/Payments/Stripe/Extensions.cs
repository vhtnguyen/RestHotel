
using Hotel.Shared.Payments.Stripe.Checkout;
using Hotel.Shared.Payments.Stripe.Link;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stripe;
using Stripe.Checkout;

namespace Hotel.Shared.Payments.Stripe;
/*
Stripe cli: 
    - installation: docker run --rm -it stripe/stripe-cli:latest
    - login: stripe login --api-key <key>
    - listen: stripe listen --forward-to <local web webhook url>
    - publish: stripe trigger <evnt>
 
*/
public static class Extensions
{
    // used on your application
    public static IServiceCollection AddStripePayment(this IServiceCollection services)
    {
        // resolve configuration
        using var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetService<IConfiguration>()!;

        // bind options
        var options = new StripeOptions();
        var section = configuration.GetSection("stripe");
        section.Bind(options);
        services.Configure<StripeOptions>(section);

        // setup key
        StripeConfiguration.ApiKey = options.ApiKey;

        services.AddScoped<IStripePaymentClient, StripePaymentClient>();
        services.AddScoped<TokenService>();
        services.AddScoped<CustomerService>();
        services.AddScoped<ChargeService>();

        return services;
    }

    // used on webhook
    public static IServiceCollection AddStripeCheckout(this IServiceCollection services)
    {
        // resolve configuration
        using var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetService<IConfiguration>()!;

        // bind options
        var options = new StripeOptions();
        var section = configuration.GetSection("stripe");
        section.Bind(options);
        services.Configure<StripeOptions>(section);

        // setup key
        StripeConfiguration.ApiKey = options.ApiKey;

        services.AddScoped<SessionService>();
        services.AddScoped<IStripeCheckoutClient, StripeCheckoutClient>();
        return services;
    }
}
