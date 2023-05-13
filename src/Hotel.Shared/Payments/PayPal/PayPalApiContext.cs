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
        return ConfigManager.Instance.GetProperties();
    }

    private string GetAccessToken()
    {
        string accessToken = new OAuthTokenCredential(_options.ClientId, _options.ClientSecret, GetConfig()).GetAccessToken();
        return accessToken;
    }
    public Task<APIContext> GetApiContext()
    {
        var apiContext = new APIContext(GetAccessToken());
        apiContext.Config = GetConfig();

        return Task.FromResult(apiContext);
    }
}
