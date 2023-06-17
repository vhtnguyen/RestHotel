using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Hotel.Shared.Helpers;

namespace Hotel.BusinessLogic.DTO.HotelReservation
{
    public class ReservationCreateDTO
    {
        public string? Email { get; set; }
        public float TotalSum { get; set; }
        public float DownPayment { get; set; }
        public string? NameCus { get; set; }
        public string? ArrivalDateStr { get; set; }
        public string? DepartureDateStr { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime Date { get; set; }
        public ICollection<GuestRoomInfoDTO> ReservationCards { get; set; } = new List<GuestRoomInfoDTO>();

        // public ReservationCreateDTO(string email, float totalSum, 
        //     float downPayment, string nameCus, string? arrivalDate, 
        //     string? departureDate, List<GuestRoomInfoDTO> roomsList)
        // {
        //     Email = email;
        //     TotalSum = totalSum;
        //     DownPayment = downPayment;
        //     NameCus = nameCus;
        //     ReservationCards = roomsList;
        //     ArrivalDateStr = arrivalDate;
        //     DepartureDateStr = departureDate;
        //     //Date = DateTime.UtcNow;
        // }
        public Boolean ParseDate()
        {
            DateTime Arrival;
            DateTime Departure;
            bool isParsearrivalDate = DateTime.TryParseExact(ArrivalDateStr, "dd/MM/yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out Arrival);
            bool isDepartureDate = DateTime.TryParseExact(DepartureDateStr, "dd/MM/yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out Departure);


            //Console.WriteLine(isDepartureDate);
            //Console.WriteLine(isParsearrivalDate);

            if (!(isParsearrivalDate & isDepartureDate))
            {
                return false;
            }
            this.ArrivalDate = Arrival;
            this.DepartureDate = Departure;
            this.Date = DateTime.UtcNow.ToVietnameseDatetime();
            return true;
        }
    }
}
