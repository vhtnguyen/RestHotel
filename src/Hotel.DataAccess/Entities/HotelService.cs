using Newtonsoft.Json;

namespace Hotel.DataAccess.Entities; 
public class HotelService 
{

    public int Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }

    // reference key
    public ServiceCategory? Category { get; set; }
    public virtual ICollection<InvoiceHotelService> Invoices { get; set; } = new List<InvoiceHotelService>();

    [JsonConstructor]
    public HotelService(int id, string? name, double price)
    {
        Id = id;
        Name = name;
        Price = price;
    }
}
