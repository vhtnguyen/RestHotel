using Hotel.BusinessLogic.Commands;
using Hotel.BusinessLogic.DTO.Payment;
using Hotel.BusinessLogic.Services.IServices;
using Hotel.DataAccess.Repositories.IRepositories;
using Hotel.Shared.Exceptions;
using Hotel.Shared.Helpers;
using Hotel.Shared.Payments;
using Hotel.Shared.Redis;
using Microsoft.Extensions.Options;

namespace Hotel.BusinessLogic.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentFactory _factory;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly ICacheService _cacheService;
    private readonly PaymentOptions _options;
    private readonly IStreamingPublisher _publisher;
    public PaymentService(
        IStreamingPublisher publisher,
        IPaymentFactory factory,
        IInvoiceRepository repo,
        ICacheService cacheService,
        IOptions<PaymentOptions> options
    )
    {
        _factory = factory;
        _invoiceRepository = repo;
        _cacheService = cacheService;
        _options = options.Value;
        _publisher = publisher;
    }
    public async Task<SessionResource> CreatePaymentLink(int invoiceId, CreatePaymentDto payment)
    {
        var invoice = await _invoiceRepository.GetInvoiceDetail(invoiceId);

        if (invoice == null)
        {
            throw new DomainBadRequestException($"Invoice has't exsited on id '{invoiceId}'", "not_found_invoice");
        }

        if (invoice.PaymentId != null)
        {
            throw new DomainBadRequestException($"Invoice has't been paid at id '{invoiceId}'", "invoice_has_paid");
        }

        var paymentSession = _factory.CreatePaymentCheckoutSession(payment.PayMethod);
        if (paymentSession == null)
        {
            throw new DomainBadRequestException(
                $"Not support paying with method '{payment.PayMethod}'",
                "payment_method_not_supported");
        }

        var sessionItems = new List<CreateSessionItemResouce>();

        double downPayment = 0;
        foreach (var card in invoice.ReservationCards)
        {
            var totalDay = card.DepartureDate.Subtract(card.ArrivalDate).Days + 1;

            sessionItems.Add(new CreateSessionItemResouce(
                $"Room_{card.Room.Id}", card.Room.RoomDetail.Price * _options.DepositRatio, totalDay));

            downPayment += card.Room.RoomDetail.Price * _options.DepositRatio * totalDay;
        }

        foreach (var service in invoice.HotelServices)
        {
            var totalDay =
                (int)DateTime.UtcNow.ToVietnameseDatetime().Subtract(service.CreateOn).Days + 1;
            sessionItems.Add(new CreateSessionItemResouce(
                service.HotelService.Name!,
                service.HotelService.Price * _options.DepositRatio, totalDay));

            downPayment += service.HotelService.Price * _options.DepositRatio * totalDay;
        }

        var sessionResource = new CreateSessionResource(
            invoiceId.ToString(),
            invoiceId.ToString(), "USD",
            invoice.TotalSum, sessionItems);

        var response = await paymentSession.CreateSession(sessionResource);

        invoice.SetPayment(response.RequestId, downPayment);
        await _invoiceRepository.SaveChangesAsync();
        // set ttl
        var expireAt = DateTime.Now.AddMinutes(_options.ExpirationAt);
        await _cacheService.SetAsync($"payment:{response.RequestId}", $"{invoiceId}", expireAt);
        return response;
    }

    public async Task<bool> GetPaymentStatus(string paymentId)
    {
        var invoice = await _invoiceRepository.FindAsync(i => i.PaymentId == paymentId);

        if (invoice == null)
        {
            throw new DomainBadRequestException($"Invoice has't exsited on id '{paymentId}'", "not_found_invoice");
        }

        if (invoice.Status == "partly_deposited")
        {
            return true;
        }

        return false;
    }

    public async Task PayFailed(string paymentIntentId)
    {
        var invoice = await _invoiceRepository.FindAsync(i => i.PaymentId == paymentIntentId);
        if (invoice == null)
        {
            throw new DomainBadRequestException($"Not found invoice at payment id '{paymentIntentId}'", "not_found_invoice");
        }
        await _invoiceRepository.RemoveInvoice(invoice);
        await _cacheService.DeleteAsync($"payment:{paymentIntentId}");
    }

    public async Task PaySucceed(string paymentIntentId)
    {
        var invoice = await _invoiceRepository.FindAsync(i => i.PaymentId == paymentIntentId);
        if (invoice == null)
        {
            throw new DomainBadRequestException($"Not found invoice at payment id '{paymentIntentId}'", "not_found_invoice");
        }
        invoice.PaySucceed();
        await _invoiceRepository.SaveChangesAsync();
        await _cacheService.DeleteAsync($"payment:{paymentIntentId}");

        var details = new List<InvoiceDetail>();

        foreach (var card in invoice.ReservationCards)
        {
            details.Add(new InvoiceDetail
            {
                Name = $"Card_{card.Id}",
                Price = card.Room.RoomDetail.Price * _options.DepositRatio,
                Quantity = 1
            });
        }

        foreach (var service in invoice.HotelServices)
        {
            details.Add(new InvoiceDetail
            {
                Name = service.HotelService.Name!,
                Price = service.HotelService.Price * _options.DepositRatio,
                Quantity = 1
            });
        }

        var command = new SendNotificationCommand
        {
            InvoiceId = invoice.Id,
            CusName = invoice.NameCus!,
            Email = invoice.Email!,
            Details = details

        };
        await _publisher.PublishAsync("email", command);
    }
}