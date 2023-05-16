using Newtonsoft.Json;

namespace Hotel.DataAccess.Entities; 
public class HotelService 
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }

    // reference key
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [JsonConstructor]
    public HotelService(int id, string? name, double price)
    {
        Id = id;
        Name = name;
        Price = price;
    }
}
