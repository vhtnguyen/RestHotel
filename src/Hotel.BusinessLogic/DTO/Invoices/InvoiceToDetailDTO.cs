using Hotel.BusinessLogic.DTO.HotelReservation;

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
    public ICollection<ReservationCardReturnDTO>? ReservationCards { get; set; }
    //public string 
}

public class ServiceToDetailDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public string? CreateOn { get; set; }
}
