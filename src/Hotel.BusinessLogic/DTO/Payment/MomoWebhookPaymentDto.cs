namespace Hotel.BusinessLogic.DTO.Payment;

public class MomoWebhookPaymentDto
{
    public string requestId { get; set; } = null!;
    public int resultCode { get; set; }
}