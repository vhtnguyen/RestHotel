using Hotel.BusinessLogic.Commands;
using Hotel.DataAccess.Repositories.IRepositories;
using Hotel.Shared.Exceptions;
using Hotel.Shared.Handlers;
using Microsoft.Extensions.Logging;
namespace Hotel.BussinessLogic.Handlers;

public class InvoiceExpirationCommandHandler : ICommandHandler<InvoiceExpirationCommand>
{
    private readonly ILogger<InvoiceExpirationCommandHandler> _logger;
    private readonly IInvoiceRepository _invoiceRepository;
    public InvoiceExpirationCommandHandler(
        IInvoiceRepository invoiceRepository,
        ILogger<InvoiceExpirationCommandHandler> logger)
    {
        _logger = logger;
        _invoiceRepository = invoiceRepository;
    }
    public async Task HandleAsync(InvoiceExpirationCommand command)
    {
        _logger.LogInformation($"handling invoice expiration ${command.payment}");

        var invoice = await _invoiceRepository.FindAsync(i => i.PaymentId == command.payment);
        if (invoice == null)
        {
            throw new DomainBadRequestException($"Not found invoice at payment id '{command.payment}'", "not_found_invoice");
        }
        await _invoiceRepository.RemoveInvoice(invoice);
        _logger.LogInformation($"handled invoice expiration ${command.payment}");
    }
}
