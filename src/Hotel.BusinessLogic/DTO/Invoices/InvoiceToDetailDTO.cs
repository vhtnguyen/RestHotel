using Hotel.BusinessLogic.DTO;
using Hotel.BusinessLogic.DTO.HotelServices;
using Hotel.BusinessLogic.DTO.ReservationCard;

namespace Hotel.BusinessLogic.DTO.Invoices;

public class InvoiceToDetailDTO
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string? Status { get; set; }
    public double TotalSum { get; set; }
    public double DownPayment { get; set; }
    public string? Email { get; set; }
    public string? NameCus { get; set; }

    public ICollection<ServiceToDetailDTO>? HotelServices { get; set; }
    public ICollection<ReservationCardToDetailDTO>? ReservationCards { get; set; }
    //public string 
}
