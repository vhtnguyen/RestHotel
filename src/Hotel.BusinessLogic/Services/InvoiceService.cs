using AutoMapper;
using Hotel.BusinessLogic.DTO.Invoices;
using Hotel.DataAccess.Entities;
using Hotel.DataAccess.ObjectValues;
using Hotel.DataAccess.Repositories.IRepositories;
using Hotel.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.Services;

internal class InvoiceService : IInvoiceService
{
    private readonly IMapper _mapper;
    private readonly IInvoiceRepository _invoiceRepository;
    public InvoiceService(IMapper mapper, IInvoiceRepository invoiceRepository)
    {
        _mapper = mapper;
        _invoiceRepository = invoiceRepository;
    }

    public async Task<List<InvoiceToGetAllDTO>> GetAllInvoiceAsync()
    {
        var result = await _invoiceRepository.GetAllInvoice();
        return _mapper.Map<List<InvoiceToGetAllDTO>>(result);
    }

    public async Task<InvoiceToDetailDTO> GetDetailDTO(int orderId)
    {
        //var conditionQuery = _mapper.Map<Invoice>(invoiceDetailDTO);
        //var result = await _invoiceRepository.GetInvoiceDetail(conditionQuery.Id);
        var result = await _invoiceRepository.GetInvoiceDetail(orderId);
        return _mapper.Map<InvoiceToDetailDTO>(result);
    }

    public Task<Invoice> GetInvoiceBrowser(InvoiceBrowserDTO query)
    {
        //var req_query = _mapper.Map<Invoice>(query);
        //var result = _invoiceRepository.
        throw new NotImplementedException();
    }

    public Task UpdateInvocie()
    {
        throw new NotImplementedException();
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
            double roomFee = daysOfStay * card.Room!.RoomDetail!.Price;
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

            total = total + roomFee;
            log = log + " - " + roomFee.ToString();
            detailInvoice.Add(log);
        }

        foreach (InvoiceHotelService service in invoice.HotelServices)
        {
            total = total + service.HotelService.Price;
            string log = service.HotelService.Name + " - " + service.HotelService.Price.ToString() + "\n";
            detailInvoice.Add(log);
        }
        
        invoice.TotalSum = total;

        await _invoiceRepository.UpdateInvoice(invoice);

        return (total, detailInvoice);
    }
}


