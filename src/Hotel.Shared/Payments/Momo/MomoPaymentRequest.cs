using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Runtime.InteropServices.JavaScript;
using System.Text;

namespace Hotel.Shared.Payments.Momo;

internal class MomoPaymentRequest : IMomoPaymentRequest
{
    private readonly HttpClient _client;

    public MomoPaymentRequest(HttpClient client)
    {
        _client = client;
    }
    public async Task<JObject> SendPaymentRequest(string endpoint, JObject data)
    {
        using var response = await _client.PostAsJsonAsync<JObject>(endpoint, data);
        var result = await response.Content.ReadFromJsonAsync<JObject>();
        return result!;
    }
}
