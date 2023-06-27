using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hotel.Shared.Payments.Momo;
internal class MomoClient : IMomoClient
{
    //https://developers.momo.vn/v3/docs/payment/api/wallet/onetime
    private readonly IMomoPaymentRequest _momoPaymentRequest;
    private readonly IMomoSecurity _momoSecurity;
    private readonly MomoOptions _options;
    public MomoClient(
        IMomoSecurity momoSecurity,
        IMomoPaymentRequest momoPaymentRequest,
        IOptions<MomoOptions> options)
    {
        _momoSecurity = momoSecurity;
        _momoPaymentRequest = momoPaymentRequest;
        _options = options.Value;
    }

    public async Task<SessionResource> CreateSession(CreateSessionResource resource)
    {
        var payload = new MomoPayload
        {
            partnerCode = _options.PartnerCode,
            requestId = "REQUEST_" + resource.OrderId,
            amount = (long)resource.Items.Sum(s => s.Quantity * s.Price),
            orderId = "ORDER_" + resource.OrderId,
            orderInfo = "Hotel invoice",
            redirectUrl = _options.ReturnUrl,
            ipnUrl = _options.NotificationUrl,
            requestType = _options.Mode,
            extraData = "",
            lang = "vi"
        };
        string rawHash = MomoPayload.CreateRawSignature(payload, _options.AccessKey);
        string signature = _momoSecurity.signSHA256(rawHash, _options.SecretKey);

        payload.setSignture(signature);

        var response = await _momoPaymentRequest.SendPaymentRequest(_options.EndPoint, payload);

        var result = new SessionResource(response["qrCodeUrl"], response["requestId"]);
        return result;
    }
}
