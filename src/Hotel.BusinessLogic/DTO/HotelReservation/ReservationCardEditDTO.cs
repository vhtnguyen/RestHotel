using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Hotel.BusinessLogic.DTO.HotelReservation
{
    public class ReservationCardEditDTO
    {
        public int Id { get; set;}
        public string? Notes { get; set; }
        public ICollection<GuestDTO> Guests { get; set; } = new List<GuestDTO>();
    }
}
