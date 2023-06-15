using Hotel.BusinessLogic.DTO.Payment;
using Hotel.BusinessLogic.Services.IServices;
using Hotel.Shared.Payments.Stripe;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Stripe;

namespace Hotel.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;
    private readonly StripeOptions _options;
    public PaymentController(
        IOptions<StripeOptions> options,
        IPaymentService paymentService)
    {
        _paymentService = paymentService;
        _options = options.Value;
    }

    [HttpPost("{invoiceId}")]
    public async Task<ActionResult> Create(int invoiceId, CreatePaymentDto payment)
    {
        return Ok(await _paymentService.CreatePaymentLink(invoiceId, payment));
    }

    [HttpPost("stripe")]
    public async Task<ActionResult> StripeWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], _options.WebhookSecretKey);

        var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(json)!;
        var id = data["id"];
        // Handle the event
        if (stripeEvent.Type == Events.CheckoutSessionCompleted)
        {

        }
        else if (stripeEvent.Type == Events.CheckoutSessionExpired)
        {

        }
        else
        {
            Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
        }

        throw new NotImplementedException();
    }


    [HttpGet("stripe")]
    public ActionResult StripeRedirect()
    {
        throw new NotImplementedException();
    }
}
