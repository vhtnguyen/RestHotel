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

    // reference property
    public ICollection<ReservationCard> ReservationCards { get; set; } = new List<ReservationCard>();
    public ICollection<HotelService> HotelServices { get; set; } = new List<HotelService>();

    [JsonConstructor]
    public Invoice(int id, DateTime date, string? status, double totalSum, double downPayment)
    {
        Id = id;
        Date = date;
        Status = status;
        TotalSum = totalSum;
        DownPayment = downPayment;
    }

    public void AddHotelService(HotelService service)
    {
        var isExist = HotelServices.Any(s => s.Id == service.Id);
        if (isExist)
        {
            // throw exception here
        }

        HotelServices.Add(service);
    }

    public void RemoveHotelService(HotelService service)
    {
        var isExist = HotelServices.Any(s => s.Id == service.Id);
        if (!isExist)
        {
            // throw exception here
        }

        HotelServices.Remove(service);
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

    public static Revenue ViewRevenue(List<Invoice> invoices)
    {
        return new();
    }
}
