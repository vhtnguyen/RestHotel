using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Hotel.BusinessLogic.DTO.HotelReservation
{
    public class InvoiceReturnDTO
    {
        public int Id {get; set;}
        public string? Email { get; set; }
        public float TotalSum { get; set; }
        public float DownPayment { get; set;}
        public string? NameCus { get; set; }
        public string? ArrivalDate { get; set; }
        public string? DepartureDate { get; set; }
        public DateTime Date {get; set;}
    }
}
