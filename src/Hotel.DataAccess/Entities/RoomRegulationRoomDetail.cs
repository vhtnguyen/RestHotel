namespace Hotel.DataAccess.Entities;

public class RoomRegulationRoomDetail
{
    public int RoomDetailId { get; set; }
    public int RoomRegulationId { get; set; }
    public bool IsValid { get; set; }
    public RoomDetail? RoomDetail { get; set; }
    public RoomRegulation? RoomRegulation { get; set; } 
}
