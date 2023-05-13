using Microsoft.Extensions.Options;
using Stripe.Checkout;

namespace Hotel.Shared.Payments.Stripe.Link;

internal class StripeCheckoutClient : IStripeCheckoutClient
{
    private readonly SessionService _sessionService;
    private readonly StripeOptions _options;
    public StripeCheckoutClient(
        SessionService sessionService,
        IOptions<StripeOptions> options)
    {
        _sessionService = sessionService;
        _options = options.Value;
    }
    public async Task<SessionResource> CreateSession(CreateSessionResource resource)
    {
        var options = new SessionCreateOptions
        {
            LineItems = (List<SessionLineItemOptions>)resource.Items.Select(
                i => new SessionLineItemOptions { 
                    Price = i.Price.ToString(), Quantity = i.Quantity,
                    PriceData = new SessionLineItemPriceDataOptions {  Product = i.Name }
                }),
            Mode = _options.Mode,
            SuccessUrl = _options.SuccessUrl + $":requestId={resource.RequestId}",
            CancelUrl = _options.CancelUrl,
            Currency = resource.Currency
        };

        var session = await _sessionService.CreateAsync(options);
        var result = new SessionResource(session.Url);
        return result;
    }
}
