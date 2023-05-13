using Newtonsoft.Json.Linq;

namespace Hotel.Shared.Payments.Momo;

public interface IMomoPaymentRequest
{
    Task<JObject> SendPaymentRequest(string endpoint, JObject data);
}
