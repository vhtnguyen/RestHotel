namespace Hotel.Shared.Payments.Momo;

public class MomoPayload
{
    public string partnerCode { get; set; } = null!;
    public string requestId { get; set; } = null!;
    public long amount { get; set; }
    public string orderId { get; set; } = null!;
    public string orderInfo { get; set; } = null!;
    public string redirectUrl { get; set; } = null!;
    public string ipnUrl { get; set; } = null!;
    public string requestType { get; set; } = null!;
    public string extraData { get; set; } = null!;
    public string lang { get; set; } = null!;
    public string? signature { get; set; }

    public void setSignture(string signature)
    {
        this.signature = signature;
    }
    public static string CreateRawSignature(MomoPayload payload, string accessKey)
    {
        return $"accessKey={accessKey}&amount={payload.amount}&extraData={payload.extraData}" +
        $"&ipnUrl={payload.ipnUrl}&orderId={payload.orderId}&orderInfo={payload.orderInfo}" +
        $"&partnerCode={payload.partnerCode}&redirectUrl={payload.redirectUrl}" +
        $"&requestId={payload.requestId}&requestType={payload.requestType}";
    }
}