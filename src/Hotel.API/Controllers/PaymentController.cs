using Hotel.BusinessLogic.DTO.Payment;
using Hotel.BusinessLogic.Services.IServices;
using Hotel.Shared.Payments.Stripe;
using Hotel.Shared.Redis;
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
    private readonly RedisOptions _redisOptions;
    private readonly ILogger<PaymentController> _logger;
    public PaymentController(
        ILogger<PaymentController> logger,
        IOptions<StripeOptions> options,
        IOptions<RedisOptions> redisOptions,
        IPaymentService paymentService)
    {
        _paymentService = paymentService;
        _options = options.Value;
        _redisOptions = redisOptions.Value;
        _logger = logger;
    }

    [HttpPost("{invoiceId}")]
    public async Task<ActionResult> Create(int invoiceId, CreatePaymentDto payment)
    {
        var response = await _paymentService.CreatePaymentLink(invoiceId, payment);
        return Ok(new { Url = response.Url, ExpireAt = _redisOptions.ExpirationAt });
    }

    [HttpPost("stripe")]
    public async Task<ActionResult> StripeWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        var stripeEvent = EventUtility.ConstructEvent(json,
                    HttpContext.Request.Headers["Stripe-Signature"], _options.WebhookSecretKey);

        var _object = JsonConvert.DeserializeObject<StripeWebhookPaymentDto>(json)!;
        var id = _object.data.Object.id;
        // Handle the event
        _logger.LogInformation($"handling event {stripeEvent.Type} webhook");
        if (stripeEvent.Type == Events.CheckoutSessionCompleted)
        {
            await _paymentService.PaySucceed(id);
        }
        else if (stripeEvent.Type == Events.CheckoutSessionExpired)
        {
            await _paymentService.PayFailed(id);
        }
        else
        {
            Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
            return BadRequest();
        }
        _logger.LogInformation($"handled event {stripeEvent.Type} webhook");
        return NoContent();
    }


    [HttpGet("stripe")]
    public ActionResult StripeRedirect()
    {
        return Ok("pay success or fail");
    }

    [HttpPost("paypal")]
    public async Task<ActionResult> PaypalWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        var payment = JsonConvert.DeserializeObject<PaypalWebhookPaymentDto>(json)!;

        _logger.LogInformation($"handling event {payment.event_type} webhook");
        if (payment.event_type == "PAYMENTS.PAYMENT.CREATED")
        {
            await _paymentService.PaySucceed(payment.resource.id);
        }
        // else if (payment.event_type == "PAYMENT.SALE.DENIED")
        // {
        //     await _paymentService.PayFailed(payment.resource.id);
        // }
        else
        {
            Console.WriteLine("Unhandled event type: {0}", payment.event_type);
            return BadRequest();
        }

        _logger.LogInformation($"handled event {payment.event_type} webhook");
        return NoContent();
    }

    [HttpGet("paypal")]
    public ActionResult PaypalRedirect()
    {
        Console.WriteLine("get");
        return Ok("pay success or fail");
    }

    [HttpPost("momo")]
    public async Task<ActionResult> MomoWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        var payment = JsonConvert.DeserializeObject<MomoWebhookPaymentDto>(json)!;

        _logger.LogInformation($"handling event {payment.resultCode} webhook");
        if (payment.resultCode == 0)
        {
            await _paymentService.PaySucceed(payment.requestId);
        }
        else if (payment.resultCode > 0)
        {
            await _paymentService.PayFailed(payment.requestId);
        }
        else
        {
            Console.WriteLine("Unhandled event type: {0}", payment.resultCode);
            return BadRequest();
        }

        _logger.LogInformation($"handled event {payment.resultCode} webhook");
        return NoContent();
    }

    [HttpGet("momo")]
    public ActionResult MomoRedirect()
    {
        return Ok("pay success or fail");
    }
}
