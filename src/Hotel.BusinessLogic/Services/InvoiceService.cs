using AutoMapper;
using Hotel.BusinessLogic.DTO.Invoices;
using Hotel.BusinessLogic.Services.IServices;
using Hotel.DataAccess.Repositories.IRepositories;
using Hotel.Shared.Exceptions;

namespace Hotel.BusinessLogic.Services;

internal class InvoiceService : IInvoiceService
{
    private readonly IMapper _mapper;
    private readonly IInvoiceRepository _invoiceRepository;
    public InvoiceService(
        IMapper mapper,
        IInvoiceRepository invoiceRepository,
        IReservationService reservationRepository,
        IHotelServiceRepository serviceRepository)
    {
        _mapper = mapper;
        _invoiceRepository = invoiceRepository;
    }

    public async Task AddReservationCard(int invoiceId, int cardId)
    {
        var invoice = await _invoiceRepository.GetInvoiceDetail(invoiceId);
        if (invoice == null)
        {
            throw new DomainBadRequestException($"Invoice has't exsited on id '{invoiceId}'", "not_found_invoice");
        }

        // get reservation card
        await _invoiceRepository.SaveChangesAsync();
    }

    public async Task AddService(int invoiceId, int serviceId)
    {
        var invoice = await _invoiceRepository.GetInvoiceDetail(invoiceId);
        if (invoice == null)
        {
            throw new DomainBadRequestException($"Invoice has't exsited on id '{invoiceId}'", "not_found_invoice");
        }
    }

    public async Task<IEnumerable<InvoiceToGetAllDTO>> GetAllInvoiceAsync()
    {
        var result = await _invoiceRepository.GetAllInvoice();
        return _mapper.Map<IEnumerable<InvoiceToGetAllDTO>>(result);
    }

    public async Task<InvoiceToDetailDTO> GetDetailDTO(int orderId)
    {
        var result = await _invoiceRepository.GetInvoiceDetail(orderId);
        return _mapper.Map<InvoiceToDetailDTO>(result);
    }

    public async Task RemoveReservationCard(int invoiceId, int cardId)
    {
        var invoice = await _invoiceRepository.GetInvoiceDetail(invoiceId);
        if (invoice == null)
        {
            throw new DomainBadRequestException($"Invoice has't exsited on id '{invoiceId}'", "not_found_invoice");
        }
    }

    public async Task RemoveService(int invoiceId, int serviceId)
    {
        var invoice = await _invoiceRepository.GetInvoiceDetail(invoiceId);
        if (invoice == null)
        {
            throw new DomainBadRequestException($"Invoice has't exsited on id '{invoiceId}'", "not_found_invoice");
        }
    }
}


