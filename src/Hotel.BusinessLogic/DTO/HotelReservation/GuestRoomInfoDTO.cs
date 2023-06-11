using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.DTO.HotelReservation
{
    public class GuestRoomInfoDTO
    {
        public int RoomId { get; set; }
        public string? Notes { get; set; }
        public int TotalGuests { get; set; }
        public List<GuestDTO> GuestsInfo { get; set; } = new List<GuestDTO>();

        public GuestRoomInfoDTO(int roomId, string? notes, int totalGuests, List<GuestDTO> guestsInfo)
        {
            RoomId = roomId;
            Notes = notes;
            TotalGuests = totalGuests;
            GuestsInfo = guestsInfo;
        }
    }
}
