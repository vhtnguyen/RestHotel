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
    private readonly IReservationRepository _reservationRepository;
    private readonly IHotelServiceRepository _serviceRepository;
    public InvoiceService(
        IMapper mapper,
        IInvoiceRepository invoiceRepository,
        IReservationRepository reservationRepository,
        IHotelServiceRepository serviceRepository)
    {
        _mapper = mapper;
        _invoiceRepository = invoiceRepository;
        _reservationRepository = reservationRepository;
        _serviceRepository = serviceRepository;
    }

    public async Task AddReservationCard(int invoiceId, int cardId)
    {
        var invoice = await _invoiceRepository.GetInvoiceDetail(invoiceId);
        if (invoice == null)
        {
            throw new DomainBadRequestException($"Invoice has't exsited on id '{invoiceId}'", "not_found_invoice");
        }

        var card = await _reservationRepository.GetAsync(cardId);

        if (card == null)
        {
            throw new DomainBadRequestException($"Card doen't exist on id '{cardId}'", "not_found_card");
        }
        invoice.AddReservationCard(card);
        invoice.TotalSum += card.Room.RoomDetail.Price;
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

        var service = await _serviceRepository.GetAsync(serviceId);

        if (service == null)
        {
            throw new DomainBadRequestException($"service doen't exist on id '{serviceId}'", "not_found_service");
        }

        invoice.TotalSum += service.Price;
        invoice.AddHotelService(service);
        await _invoiceRepository.SaveChangesAsync();
    }

    public async Task Delete(int invoiceId)
    {
        var invoice = await _invoiceRepository.GetInvoiceDetail(invoiceId);
        if (invoice == null)
        {
            throw new DomainBadRequestException($"Invoice has't exsited on id '{invoiceId}'", "not_found_invoice");
        }

        await _invoiceRepository.RemoveInvoice(invoice);
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

        var card = await _reservationRepository.GetAsync(cardId);

        if (card == null)
        {
            throw new DomainBadRequestException($"Card doen't exist on id '{cardId}'", "not_found_card");
        }

        invoice.RemoveReservationCard(card);
        invoice.TotalSum -= card.Room.RoomDetail.Price;
        // get reservation card
        await _invoiceRepository.SaveChangesAsync();
    }

    public async Task RemoveService(int invoiceId, int serviceId)
    {
        var invoice = await _invoiceRepository.GetInvoiceDetail(invoiceId);
        if (invoice == null)
        {
            throw new DomainBadRequestException($"Invoice has't exsited on id '{invoiceId}'", "not_found_invoice");
        }

        var service = await _serviceRepository.GetAsync(serviceId);

        if (service == null)
        {
            throw new DomainBadRequestException($"service doen't exist on id '{serviceId}'", "not_found_service");
        }

        invoice.TotalSum -= service.Price;
        invoice.RemoveHotelService(service);
        await _invoiceRepository.SaveChangesAsync();
    }
}


