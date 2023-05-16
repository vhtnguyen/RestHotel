using Hotel.Shared.Payments.Stripe;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;

namespace Hotel.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly StripeOptions _options;
    public PaymentController(
        IOptions<StripeOptions> options)
    {
        _options = options.Value;
    }
    [HttpGet("stripe")]
    public IActionResult Get() => Ok(_options.WebhookSecretKey);
    
    [HttpPost("stripe")]
    public async Task<IActionResult> WebhookEndpoint()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        var stripeEvent = EventUtility.ConstructEvent(json,
                Request.Headers["Stripe-Signature"], _options.WebhookSecretKey);

        if (stripeEvent.Type == Events.PaymentIntentSucceeded)
        {
            Console.WriteLine($"handling event {stripeEvent.Type}");
        }
        else
        {
            Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
        }
        // Handle the event
        Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);

        return Ok("test success");
    }

    // mỗi lần giao dịch thành công, sẽ trả về trang webhook này
    // cập nhật dữ liệu trên post method thay vì success url 

    [HttpPost("paypal")]
    public IActionResult Webhook()
    {
        // -> sent from paypal with deploy link
        
        return Ok("test success");
    }
}
