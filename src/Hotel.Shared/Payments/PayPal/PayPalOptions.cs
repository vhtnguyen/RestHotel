namespace Hotel.Shared.Payments.PayPal;

public class PayPalOptions
{
    public string Mode { get; set; } = null!;
    public string Sandbox { get; set; } = null!;
    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
    public string SuccessUrl { get; set; } = null!;
    public string CancelUrl { get; set; } = null!; 
}
