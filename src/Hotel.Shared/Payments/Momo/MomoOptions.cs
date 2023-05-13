namespace Hotel.Shared.Payments.Momo;

public class MomoOptions 
{
    public string EndPoint { get; set; } = null!;
    public string PartnerCode { get; set; } = null!;
    public string AccessKey { get; set; } = null!;
    public string SecretKey { get; set; } = null!;
    public string ReturnUrl { get; set; } = null!;
    public string NotificationUrl { get; set; } = null!;
    public string Mode { get; set; } = null!;
}
