using Newtonsoft.Json;

namespace Hotel.DataAccess.Entities;

public class Room
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public string? Status { get; set; }
    public string? Note { get; set; }

    // reference key
    public virtual ICollection<ReservationCard> ReservationCards { get; set; } = new List<ReservationCard>();
    public RoomRegulation? RoomRegulation { get; set; }
    [JsonConstructor]
    public Room(int id, string? description, string? image, string? status, string? note)
    {
        Id = id;
        Description = description;
        Image = image;
        Status = status;
        Note = note;
    }

    public void SetRegulation(RoomRegulation roomRegulation)
    {
        RoomRegulation = roomRegulation;
    }
}
