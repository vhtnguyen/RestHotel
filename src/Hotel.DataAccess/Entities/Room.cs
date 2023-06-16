using Newtonsoft.Json;

namespace Hotel.DataAccess.Entities;

public class Room
{
    public int Id { get; set; }
    public string? Status { get; set; }
    public string? Note { get; set; }

    // reference key
    public virtual ICollection<ReservationCard> ReservationCards { get; set; } = new List<ReservationCard>();
    public RoomDetail RoomDetail { get; set; } = null!;
    [JsonConstructor]
    public Room(int id, string? status, string? note)
    {
        Id = id;
        Status = status;
        Note = note;
    }
}
