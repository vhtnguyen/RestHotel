using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;

namespace Hotel.Shared.Payments.Momo;

internal class MomoPaymentRequest : IMomoPaymentRequest
{
    private readonly HttpClient _client;

    public MomoPaymentRequest(HttpClient client)
    {
        _client = client;
    }
    public async Task<Dictionary<string, string>> SendPaymentRequest(string endpoint, MomoPayload data)
    {
        var response = await _client.PostAsJsonAsync(endpoint, data);
        var result = await response.Content.ReadAsStringAsync();
        Console.WriteLine(result);

        return JsonConvert.DeserializeObject<Dictionary<string, string>>(result)!;
    }
}
