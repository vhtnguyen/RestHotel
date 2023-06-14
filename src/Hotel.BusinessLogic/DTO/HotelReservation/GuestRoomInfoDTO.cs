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
        public ICollection<GuestDTO> Guests { get; set; } = new List<GuestDTO>();

        // public GuestRoomInfoDTO(int? roomId, string? notes, List<GuestDTO>? guestsInfo)
        // {
        //     RoomId = roomId;
        //     Notes = notes;
        //     Guests = guestsInfo;
        // }
    }
}
