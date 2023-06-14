using Hotel.DataAccess.ObjectValues;
using Newtonsoft.Json;

namespace Hotel.DataAccess.Entities;

public class ReservationCard
{
    public int Id { get; set; }
    public DateTime ArrivalDate { get; set; }
    public DateTime DepartureDate { get; set; }
    public string? Notes {get; set;}

    // reference key
    public Invoice? Invoice { get; set; }
    public Room? Room { get; set; }
    public virtual ICollection<Guest> Guests { get; set; } = new List<Guest>();

    [JsonConstructor]
    public ReservationCard(int id, DateTime arrivalDate, DateTime departureDate, string notes)
    {
        Id = id;
        ArrivalDate = arrivalDate;
        DepartureDate = departureDate;
        Notes = notes;
    }

    public ReservationCard() { }

    // some method
    public void SetRoom(Room room)
    {
        Room = room;
    }

    public void SetInvoice(Invoice invoice)
    {
        Invoice = invoice;
    }

    public void AddGuest(Guest guest)
    {
        //TODO: implement method
    }

    public void RemoveGuest(Guest guest)
    {
        //TODO: implement method
    }

}
