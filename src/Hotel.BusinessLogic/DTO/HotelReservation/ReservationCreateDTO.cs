using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.DTO.HotelReservation
{
    public class ReservationCreateDTO
    {
        public string? ArrivalDate { get; set; }
        public string? DepartureDate { get; set; }
        public string? Email { get; set; }
        public int Cost { get; set; }
        public List<GuestRoomInfoDTO> RoomsList { get; set; } = new List<GuestRoomInfoDTO>();

        public ReservationCreateDTO(string arrivalDate, string departureDate, int cost, List<GuestRoomInfoDTO> roomsList)
        {
            ArrivalDate = arrivalDate;
            DepartureDate = departureDate;
            Cost = cost;
            RoomsList = roomsList;
        }
    }
}
