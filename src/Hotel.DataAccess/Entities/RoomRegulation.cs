using Newtonsoft.Json;

namespace Hotel.DataAccess.Entities;

public class RoomRegulation
{
    public int Id { get; set; }
    public string? RoomType { get; set; }
    public double Price { get; set; }
    public int MaxGuest { get; set; }
    public int DefaultGuest { get; set; }
    public double MaxSurchargeRatio { get; set; }
    public double MaxOverseaSurchargeRatio { get; set; }
    public DateTime Date { get; set; }
    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
    [JsonConstructor]
    public RoomRegulation(int id, string? roomType, double price, int maxGuest, int defaultGuest, double maxSurchargeRatio, double maxOverseaSurchargeRatio, DateTime date)
    {
        Id = id;
        RoomType = roomType;
        Price = price;
        MaxGuest = maxGuest;
        DefaultGuest = defaultGuest;
        MaxSurchargeRatio = maxSurchargeRatio;
        MaxOverseaSurchargeRatio = maxOverseaSurchargeRatio;
        Date = date;
    }
}
