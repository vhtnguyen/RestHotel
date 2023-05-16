using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hotel.Shared.Payments.Momo;
internal class MomoClient : IMomoClient
{
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
        string rawHash = "partnerCode=" +
                _options.PartnerCode + "&accessKey=" +
                _options.AccessKey + "&requestId=" +
                resource.RequestId + "&amount=" +
                resource.Items.Sum(s => s.Quantity * s.Price) + "&orderId=" +
                resource.OrderId + "&returnUrl=" +
                _options.ReturnUrl + "&notifyUrl=" +
                _options.NotificationUrl + "&extraData=" +
                JsonConvert.SerializeObject(resource.Items);

        string signature = _momoSecurity.signSHA256(rawHash, _options.SecretKey);

        //build body json request
        var message = new JObject
        {
            { "partnerCode", _options.PartnerCode },
            { "accessKey", _options.AccessKey },
            { "requestId", resource.RequestId },
            { "amount", resource.Items.Sum(s => s.Quantity * s.Price) },
            { "orderId", resource.OrderId },
            { "returnUrl", _options.ReturnUrl },
            { "notifyUrl", _options.NotificationUrl },
            { "extraData", JsonConvert.SerializeObject(resource.Items) },
            { "requestType", _options.Mode }, //captureMoMoWallet
            { "signature", signature }
        };

        var response = await _momoPaymentRequest.SendPaymentRequest(_options.EndPoint, message);
        var result = new SessionResource(response.GetValue("payUrl")!.ToString());
        return result;
    }
}
