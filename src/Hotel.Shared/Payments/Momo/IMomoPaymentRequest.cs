using Newtonsoft.Json.Linq;

namespace Hotel.Shared.Payments.Momo;

public interface IMomoPaymentRequest
{
    Task<Dictionary<string, string>> SendPaymentRequest(string endpoint, MomoPayload data);
}
