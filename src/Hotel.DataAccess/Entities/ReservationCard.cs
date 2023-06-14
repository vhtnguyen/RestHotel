using Hotel.DataAccess.ObjectValues;
using Newtonsoft.Json;

namespace Hotel.DataAccess.Entities;

public class ReservationCard
{
    public int Id { get; set; }
    public DateTime ArrivalDate { get; set; }
    public DateTime DepartureDate { get; set; }

    // reference key
    public Invoice? Invoice { get; set; }
    public Room? Room { get; set; }
    public RoomRegulation? RoomRegulation { get; set; }
    public virtual ICollection<Guest> Guests { get; set; } = new List<Guest>();

    [JsonConstructor]
    public ReservationCard(int id, DateTime arrivalDate, DateTime departureDate)
    {
        Id = id;
        ArrivalDate = arrivalDate;
        DepartureDate = departureDate;
    }

    // some method
    public void SetRoom(Room room)
    {
        Room = room;
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
