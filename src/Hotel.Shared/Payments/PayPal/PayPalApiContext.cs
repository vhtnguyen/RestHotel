using Microsoft.Extensions.Options;
using PayPal.Api;

namespace Hotel.Shared.Payments.PayPal;

internal class PayPalApiContext : IPayPalApiContext
{
    private readonly PayPalOptions _options;
    public PayPalApiContext(
        IOptions<PayPalOptions> options)
    {
        _options = options.Value;
    }

    private Dictionary<string, string> GetConfig()
    {
        return new Dictionary<string, string>() {
            {"clientId", _options.ClientId},
            {"clientSecret", _options.ClientSecret},
            {"mode", _options.Mode}
        };
    }

    private string GetAccessToken()
    {
        string accessToken = new OAuthTokenCredential(GetConfig()).GetAccessToken();
        return accessToken;
    }
    public Task<APIContext> GetApiContext()
    {
        var apiContext = new APIContext(GetAccessToken())
        {
            Config = GetConfig()
        };

        return Task.FromResult(apiContext);
    }
}
