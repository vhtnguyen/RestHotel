namespace Hotel.Shared.Payments;

using Hotel.Shared.Payments.Momo;
using Hotel.Shared.Payments.PayPal;
using Hotel.Shared.Payments.Stripe.Link;
using Microsoft.Extensions.DependencyInjection;

internal class PaymentFactory : IPaymentFactory
{
    private readonly IServiceProvider _provider;
    public PaymentFactory(IServiceProvider serviceProvider)
    {
        _provider = serviceProvider;
    }
    public IPayment? CreatePaymentCheckoutSession(string payMethod)
    {
        var scope = _provider.CreateScope();
        switch (payMethod)
        {
            case "momo":
                return scope.ServiceProvider.GetService<IMomoClient>();
            case "paypal":
                return scope.ServiceProvider.GetService<IPayPalClient>();
            case "stripe":
                return scope.ServiceProvider.GetService<IStripeCheckoutClient>();
            default:
                return null;
        }
    }
}