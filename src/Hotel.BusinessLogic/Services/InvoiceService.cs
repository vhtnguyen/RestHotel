using AutoMapper;
using Hotel.BusinessLogic.DTO.Invoices;
using Hotel.DataAccess.Entities;
using Hotel.DataAccess.ObjectValues;
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

    public async Task<InvoiceToDetailDTO> AddReservationCard(int invoiceId, int cardId)
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
        // invoice.TotalSum += card.Room.RoomDetail.Price;
        // get reservation card
        await _invoiceRepository.SaveChangesAsync();

        return _mapper.Map<InvoiceToDetailDTO>(invoice);
    }

    public async Task<InvoiceToDetailDTO> AddService(int invoiceId, int serviceId)
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

        return _mapper.Map<InvoiceToDetailDTO>(invoice);
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

    public async Task<IEnumerable<InvoiceToGetAllDTO>> GetAllInvoiceAsync(InvoiceQueryDto query)
    {
        var result = await _invoiceRepository.GetAllInvoice(query.Take, query.Page, query.Status);
        return _mapper.Map<IEnumerable<InvoiceToGetAllDTO>>(result);
    }

    public async Task<InvoiceToDetailDTO> GetDetailDTO(int orderId)
    {
        var result = await _invoiceRepository.GetInvoiceDetail(orderId);
        if (result == null)
        {
            throw new DomainBadRequestException($"Invoice has't exsited on id '{orderId}'", "not_found_invoice");
        }
        return _mapper.Map<InvoiceToDetailDTO>(result);
    }

    public async Task<InvoiceToDetailDTO> RemoveReservationCard(int invoiceId, int cardId)
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
        // invoice.TotalSum -= card.Room.RoomDetail.Price;
        // get reservation card
        await _invoiceRepository.SaveChangesAsync();

        return _mapper.Map<InvoiceToDetailDTO>(invoice);
    }

    public async Task<InvoiceToDetailDTO> RemoveService(int invoiceId, int serviceId)
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

        return _mapper.Map<InvoiceToDetailDTO>(invoice);
    }

    public async Task<(double, List<string>)> CalculateInvoice(int id)
    {
        double total = 0;
        List<string> detailInvoice = new List<string>();

        Invoice? invoice = await _invoiceRepository.GetInvoiceDetail(id);
        if (invoice == null)
        {
            throw new DomainBadRequestException($"Invoice has't exsited on id '{id}'", "not_found_invoice");
        }

        foreach (ReservationCard card in invoice.ReservationCards)
        {
            int daysOfStay = card.DepartureDate.Day - card.ArrivalDate.Day + 1;
            int numGuests = card.Guests.Count();
            Boolean hasForeign = false;
            double roomFee = card.Room!.RoomDetail!.Price;
            string log = "Room " + card.Room.Id + " - " + numGuests + " guests";

            foreach (Guest guest in card.Guests)
            {
                if (guest.Type == "foreign")
                {
                    hasForeign = true;
                    break;
                }
            }

            if (hasForeign)
            {
                log = log + " - has foreign";
                roomFee = roomFee + roomFee * card.RoomRegulation!.MaxOverseaSurchargeRatio;
            }

            if (numGuests > card.RoomRegulation!.DefaultGuest)
            {
                log = log + " - over max guests";
                roomFee = roomFee + roomFee * card.RoomRegulation!.MaxSurchargeRatio;
            }

            roomFee = roomFee * daysOfStay;
            total = total + roomFee;
            log = log + " - " + daysOfStay + " days" + " - " + roomFee.ToString();
            detailInvoice.Add(log);
        }

        foreach (InvoiceHotelService service in invoice.HotelServices)
        {
            total = total + service.HotelService.Price;
            string log = service.HotelService.Name + " - " + service.HotelService.Price.ToString();
            detailInvoice.Add(log);
        }

        invoice.TotalSum = total;

        await _invoiceRepository.UpdateInvoice(invoice);

        return (total, detailInvoice);
    }

    public async Task<InvoiceToDetailDTO> CheckoutInvoice(int invoiceId)
    {
        var invoice = await _invoiceRepository.GetInvoiceDetail(invoiceId);
        if (invoice == null)
        {
            throw new DomainBadRequestException($"Invoice has't exsited on id '{invoiceId}'", "not_found_invoice");
        }

        invoice.Checkout();
        await _invoiceRepository.SaveChangesAsync();

        return _mapper.Map<InvoiceToDetailDTO>(invoice);
    }
}


