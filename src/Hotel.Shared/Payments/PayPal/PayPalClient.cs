using System.Globalization;
using Microsoft.Extensions.Options;
using PayPal.Api;

namespace Hotel.Shared.Payments.PayPal;

// http://paypal.github.io/PayPal-NET-SDK/Samples/PaymentWithPayPal.aspx.html
internal class PayPalClient : IPayPalClient
{
    private readonly IPayPalApiContext _context;
    private readonly PayPalOptions _options;
    public PayPalClient(
        IOptions<PayPalOptions> options,
        IPayPalApiContext context)
    {
        _context = context;
        _options = options.Value;
    }
    public async Task<SessionResource> CreateSession(CreateSessionResource resource)
    {
        // get api context
        var apiContext = await _context.GetApiContext();

        // create payer
        var payer = new Payer() { payment_method = "paypal" };

        // create item
        var itemList = new ItemList()
        {
            items = resource.Items.Select(i => new Item
            {
                quantity = i.Quantity.ToString(),
                name = i.Name,
                price = i.Price.ToString(),
                currency = resource.Currency
            }).ToList()
        };

        var total = resource.Items.Sum(i => i.Quantity * i.Price);
        var tax = resource.TotalSum - total;
        var details = new Details()
        {
            subtotal = total.ToString(),
            tax = tax.ToString()
        };
        // create amount
        var amount = new Amount()
        {
            currency = resource.Currency,
            total = resource.TotalSum.ToString("F"),
            details = details
        };

        // create redirect url
        var redirectUrl = new RedirectUrls
        {
            cancel_url = _options.ReturnUrl + $"?error=1&requestId={resource.RequestId}",
            return_url = _options.ReturnUrl + $"?error=0&requestId={resource.RequestId}"
        };

        // create transaction
        var transactions = new List<Transaction>
        {
            new Transaction()
            {
                description = "payment",
                amount = amount,
                item_list = itemList,
            }
        };

        // create payment
        var payment = new Payment()
        {
            intent = "sale",
            payer = payer,
            transactions = transactions,
            redirect_urls = redirectUrl
        };

        // send post request to paypal
        var createPayment = await Task.Run(() => payment.Create(apiContext));

        // get link with approve url
        var links = createPayment.links.GetEnumerator();

        while (links.MoveNext())
        {
            var link = links.Current;
            if (link.rel.ToLower().Trim().Equals("approval_url"))
            {
                return new SessionResource(link.href, createPayment.id);
            }
        }
        return new SessionResource("", createPayment.id);
    }
}
