using Hotel.BusinessLogic.DTO.Payment;
using Hotel.BusinessLogic.Services.IServices;
using Hotel.DataAccess.Repositories.IRepositories;
using Hotel.Shared.Exceptions;
using Hotel.Shared.Payments;
using Hotel.Shared.Redis;
using Microsoft.Extensions.Options;

namespace Hotel.BusinessLogic.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentFactory _factory;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly ICacheService _cacheService;
    private readonly RedisOptions _options;
    public PaymentService(
        IPaymentFactory factory,
        IInvoiceRepository repo,
        ICacheService cacheService,
        IOptions<RedisOptions> options
    )
    {
        _factory = factory;
        _invoiceRepository = repo;
        _cacheService = cacheService;
        _options = options.Value;
    }
    public async Task<SessionResource> CreatePaymentLink(int invoiceId, CreatePaymentDto payment)
    {
        var invoice = await _invoiceRepository.GetInvoiceDetail(invoiceId);

        if (invoice == null)
        {
            throw new DomainBadRequestException($"Invoice has't exsited on id '{invoiceId}'", "not_found_invoice");
        }

        var paymentSession = _factory.CreatePaymentCheckoutSession(payment.PayMethod);
        if (paymentSession == null)
        {
            throw new DomainBadRequestException(
                $"Not support paying with method '{payment.PayMethod}'",
                "payment_method_not_supported");
        }

        var sessionItems = new List<CreateSessionItemResouce>();

        foreach (var card in invoice.ReservationCards)
        {
            sessionItems.Add(new CreateSessionItemResouce($"Card_{card.Id}", card.Room.RoomDetail.Price, 1));
        }

        foreach (var service in invoice.HotelServices)
        {
            sessionItems.Add(new CreateSessionItemResouce(service.HotelService.Name!, service.HotelService.Price, 1));
        }

        var sessionResource = new CreateSessionResource(
            invoiceId.ToString(),
            invoiceId.ToString(), "USD", sessionItems);

        var response = await paymentSession.CreateSession(sessionResource);

        // set ttl
        var expireAt = DateTime.Now.AddMinutes(_options.ExpirationAt);
        await _cacheService.SetAsync($"payment:{response.RequestId}", $"{invoiceId}", expireAt);

        return response;
    }

    public async Task PayFailed(string paymentIntentId)
    {
        var invoiceId = await _cacheService.GetAsync<string>($"payment:{paymentIntentId}");

        if (String.IsNullOrWhiteSpace(invoiceId))
        {
            throw new DomainBadRequestException(
                $"Not found invoice id associate payment id '{paymentIntentId}'",
                "not_found_invoice");
        }

        Int32.TryParse(invoiceId, out var id);

        var invoice = await _invoiceRepository.GetInvoiceDetail(id);
        if (invoice == null)
        {
            throw new DomainBadRequestException($"Not found invoice at id '{invoiceId}'", "not_found_invoice");
        }
        await _invoiceRepository.RemoveInvoice(invoice);
        await _cacheService.DeleteAsync($"payment:{paymentIntentId}");
    }

    public async Task PaySucceed(string paymentIntentId)
    {
        var invoiceId = await _cacheService.GetAsync<string>($"payment:{paymentIntentId}");

        if (String.IsNullOrWhiteSpace(invoiceId))
        {
            throw new DomainBadRequestException(
                $"Not found invoice id associate payment id '{paymentIntentId}'",
                "not_found_invoice");
        }

        Int32.TryParse(invoiceId, out var id);

        var invoice = await _invoiceRepository.GetInvoiceDetail(id);
        if (invoice == null)
        {
            throw new DomainBadRequestException($"Not found invoice at id '{invoiceId}'", "not_found_invoice");
        }

        invoice.PaySucceed();
        await _invoiceRepository.SaveChangesAsync();
        await _cacheService.DeleteAsync($"payment:{paymentIntentId}");
    }
}