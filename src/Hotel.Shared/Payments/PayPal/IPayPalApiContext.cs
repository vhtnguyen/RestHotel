using PayPal.Api;

namespace Hotel.Shared.Payments.PayPal;

public interface IPayPalApiContext
{
    Task<APIContext> GetApiContext();
}
