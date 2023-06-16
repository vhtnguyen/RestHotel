using Microsoft.Extensions.Options;
using Stripe;
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
        StripeConfiguration.ApiKey = _options.ApiKey;
        var lineItems = new List<SessionLineItemOptions>();
        foreach (var item in resource.Items)
        {
            lineItems.Add(new SessionLineItemOptions
            {
                // Price = item.Name,
                Quantity = item.Quantity,
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long?)item.Price,
                    Currency = resource.Currency,
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.Name
                    }
                }

            });
        }
        var options = new SessionCreateOptions
        {
            LineItems = lineItems,
            Mode = _options.Mode,
            SuccessUrl = _options.ReturnUrl + $"?requestId={resource.RequestId}&error=0",
            CancelUrl = _options.ReturnUrl + $"?requestId={resource.RequestId}&error=1",
            Currency = resource.Currency
        };

        var session = await _sessionService.CreateAsync(options);
        var result = new SessionResource(session.Url, session.Id);
        return result;
    }
}
