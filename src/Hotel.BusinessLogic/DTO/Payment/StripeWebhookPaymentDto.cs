namespace Hotel.BusinessLogic.DTO.Payment;

public class StripeWebhookPaymentDto
{
    public StripeDataDto data { get; set; } = null!;
}

public class StripeDataDto
{
    public StripeObjectDto Object { get; set; } = null!;
}
public class StripeObjectDto
{
    public string id { get; set; } = null!;
}