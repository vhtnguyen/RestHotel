using Hotel.DataAccess.ObjectValues;
using Hotel.Shared.Exceptions;
using Newtonsoft.Json;

namespace Hotel.DataAccess.Entities;

public class Invoice
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string? Status { get; set; }
    public double TotalSum { get; set; }
    public double DownPayment { get; set; }
    public string? Email { get; set; }
    public string? NameCus { get; set; }

    // reference property
    public ICollection<ReservationCard> ReservationCards { get; set; } = new List<ReservationCard>();
    public ICollection<InvoiceHotelService> HotelServices { get; set; } = new List<InvoiceHotelService>();

    [JsonConstructor]
    public Invoice(int id, DateTime date, string? status, double totalSum, double downPayment, string? email, string? nameCus)
    {
        Id = id;
        Date = date;
        Status = status;
        TotalSum = totalSum;
        DownPayment = downPayment;
        Email = email;
        NameCus = nameCus;
    }

    public void PaySucceed()
    {
        Status = "succeed_paying";
    }

    public void PayFailed()
    {
        Status = "failed_paying";
    }

    public void AddHotelService(HotelService service)
    {
        var isExist = HotelServices.Any(s => s.HotelServiceId == service.Id);
        if (isExist)
        {
            // throw exception here
            throw new DomainBadRequestException($"Service has exist at id '{service.Id}'", "has_existed_service");
        }

        HotelServices.Add(new InvoiceHotelService { InvoiceId = Id, HotelServiceId = service.Id, CreateOn = DateTime.Now });
    }

    public void RemoveHotelService(HotelService service)
    {
        var isExist = HotelServices.Any(s => s.HotelServiceId == service.Id);
        if (!isExist)
        {
            throw new DomainBadRequestException($"Not found hotel service on id '{service.Id}'", "not_found_hotel_service");
        }

        HotelServices.Remove(HotelServices.First(c => c.HotelServiceId == service.Id));
    }

    public void AddReservationCard(ReservationCard card)
    {
        var isExist = ReservationCards.Any(s => s.Id == card.Id);
        if (isExist)
        {
            // throw exception here
            throw new DomainBadRequestException($"Card has exist at id '{card.Id}'", "has_existed_card");
        }

        ReservationCards.Add(card);
    }
    public void RemoveReservationCard(ReservationCard card)
    {
        var isExist = ReservationCards.Any(s => s.Id == card.Id);
        if (!isExist)
        {
            // throw exception here
            throw new DomainBadRequestException($"Not found hotel card on id '{card.Id}'", "not_found_card");
        }

        ReservationCards.Remove(card);
    }
}
