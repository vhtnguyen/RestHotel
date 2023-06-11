using Hotel.DataAccess.ObjectValues;
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
    public string NameCus { get; set; }

    // reference property
    public ICollection<ReservationCard> ReservationCards { get; set; } = new List<ReservationCard>();
    public ICollection<InvoiceHotelService> HotelServices { get; set; } = new List<InvoiceHotelService>();

    [JsonConstructor]
    public Invoice(int id, DateTime date, string? status, double totalSum, double downPayment, string? email)
    {
        Id = id;
        Date = date;
        Status = status;
        TotalSum = totalSum;
        DownPayment = downPayment;
        Email = email;
    }

    public void AddHotelService(HotelService service)
    {
        var isExist = HotelServices.Any(s => s.HotelServiceId == service.Id);
        if (isExist)
        {
            // throw exception here
        }

        HotelServices.Add(new InvoiceHotelService { InvoiceId = Id, HotelServiceId = service.Id, CreateOn = DateTime.Now});
    }

    public void RemoveHotelService(HotelService service)
    {
        var isExist = HotelServices.Any(s => s.HotelServiceId == service.Id);
        if (!isExist)
        {
            // throw exception here
        }

        HotelServices.Remove(HotelServices.First(c => c.HotelServiceId == service.Id));
    }

    public void AddReservationCard(ReservationCard card)
    {
        var isExist = ReservationCards.Any(s => s.Id == card.Id);
        if (isExist)
        {
            // throw exception here
        }

        ReservationCards.Add(card);
    }
    public void RemoveReservationCard(ReservationCard card)
    {
        var isExist = ReservationCards.Any(s => s.Id == card.Id);
        if (!isExist)
        {
            // throw exception here
        }

        ReservationCards.Remove(card);
    }

    //public static Revenue ViewRevenue(List<Invoice> invoices)
    //{
  
    //    return new();
    //}
}
