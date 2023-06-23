namespace Hotel.DataAccess.Entities;

public class RoomDetail
{
    public int Id { get; set; }
    public double Price { get; set; }
    public string? RoomType { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }

    // reference ky
    public RoomRegulation? RoomRegulation { get; set; }
    public RoomDetail(int id, double price, string? roomType, string? description, string? image)
    {
        Id = id;
        Price = price;
        RoomType = roomType;
        Description = description;
        Image = image;
    }

    public RoomDetail()
    {
    }
}
