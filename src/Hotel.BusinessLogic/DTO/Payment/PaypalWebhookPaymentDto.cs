namespace Hotel.BusinessLogic.DTO.Payment;

public class PaypalWebhookPaymentDto
{
    public string event_type { get; set; } = null!;
    public PaypalResource resource { get; set; } = null!;
}

public class PaypalResource
{
    public string id { get; set; } = null!;
}