
namespace Hotel.Shared.Payments.Stripe;

public class StripeOptions
{
    public string ApiKey { get; set; } = null!;
    public string WebhookSecretKey { get; set; } = null!;
    public string Mode { get; set; } = null!;
    public string SuccessUrl { get; set; } = null!;
    public string CancelUrl { get; set; } = null!;
}
