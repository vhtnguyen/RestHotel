namespace Hotel.DataAccess.Entities;

public class InvoiceHotelService
{
    public int InvoiceId { get; set; }
    public int HotelServiceId { get; set; }
    public DateTime CreateOn { get; set; }
    public Invoice Invoice { get; set; } = null!;
    public HotelService HotelService { get; set; } = null!;
}
