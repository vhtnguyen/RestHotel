using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Hotel.BusinessLogic.DTO.HotelReservation
{
    public class PeriodTimeDTO
    {
        public string? From { get; set; }
        public string? To { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }

        public Boolean ParseDate()
        {
            DateTime Arrival;
            DateTime Departure;
            bool isParsearrivalDate = DateTime.TryParseExact(From, "dd/MM/yyyy", 
                CultureInfo.InvariantCulture, DateTimeStyles.None, out Arrival);
            bool isDepartureDate = DateTime.TryParseExact(To, "dd/MM/yyyy", 
                CultureInfo.InvariantCulture, DateTimeStyles.None, out Departure);

            
            //Console.WriteLine(isDepartureDate);
            //Console.WriteLine(isParsearrivalDate);

            if (!(isParsearrivalDate & isDepartureDate))
            {
                return false;
            }
            this.ArrivalDate = Arrival;
            this.DepartureDate = Departure;
            return true;
        }
    }
}
