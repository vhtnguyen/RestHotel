using Newtonsoft.Json;

namespace Hotel.DataAccess.Entities;

public class RoomRegulation
{
    public int Id { get; set; }
    public int MaxGuest { get; set; }
    public int DefaultGuest { get; set; }
    public double MaxSurchargeRatio { get; set; }
    public double MaxOverseaSurchargeRatio { get; set; }
    public double RoomExchangeFee { get; set; }

    public ICollection<RoomRegulationRoomDetail> RoomDetails { get; set; } = new List<RoomRegulationRoomDetail>();
    [JsonConstructor]
    public RoomRegulation(
        int id, int maxGuest, int defaultGuest, double maxSurchargeRatio, double maxOverseaSurchargeRatio, double roomExchangeFee)
    {
        Id = id;
        MaxGuest = maxGuest;
        DefaultGuest = defaultGuest;
        MaxSurchargeRatio = maxSurchargeRatio;
        MaxOverseaSurchargeRatio = maxOverseaSurchargeRatio;
        RoomExchangeFee = roomExchangeFee;  
    }
}
