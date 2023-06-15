using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Hotel.BusinessLogic.DTO.HotelReservation
{
    public class ChangeRoomDTO
    {
        public int OldRoomId { get; set;}
        public int NewRoomId { get; set;}
        public int InvoiceId { get; set;}
        public string? FromDateStr { get; set; }
        public string? ToDateStr { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public Boolean ParseDate()
        {
            DateTime Arrival;
            DateTime Departure;
            bool isParsearrivalDate = DateTime.TryParseExact(FromDateStr, "dd/MM/yyyy", 
                CultureInfo.InvariantCulture, DateTimeStyles.None, out Arrival);
            bool isDepartureDate = DateTime.TryParseExact(ToDateStr, "dd/MM/yyyy", 
                CultureInfo.InvariantCulture, DateTimeStyles.None, out Departure);

            if (!(isParsearrivalDate & isDepartureDate))
            {
                return false;
            }
            this.From = Arrival;
            this.To = Departure;
            return true;
        }
    }
}
