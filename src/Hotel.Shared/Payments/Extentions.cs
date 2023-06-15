using Hotel.Shared.Payments.Momo;
using Hotel.Shared.Payments.PayPal;
using Hotel.Shared.Payments.Stripe;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.Shared.Payments;

public static class Extensions
{
    public static IServiceCollection AddPayment(this IServiceCollection services)
    {
        services.AddStripeCheckout();
        services.AddPayPalCheckout();
        services.AddMomoCheckout();
        services.AddScoped<IPaymentFactory, PaymentFactory>();
        return services;
    }
}