using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.DTO.HotelReservation
{
    public class ReservationCardReturnDTO
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string? GuestName { get; set; }
        public int RoomId { get; set; }
        public int GuestsNumber { get; set; }
        public string? ArrivalDate { get; set; }
        public string? DepartureDate { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }

        // public ReservationCardReturnDTO(int id, int invoiceID, string? guestName, 
        //     int roomId, int guestsNumber, DateTime from, DateTime to, 
        //     string? status, string? notes)
        // {
        //     Id = id; 
        //     InvoiceId = invoiceID; 
        //     GuestName = guestName;
        //     RoomId = roomId;
        //     GuestsNumber = guestsNumber;
        //     ArrivalDate = from;
        //     DepartureDate = to;
        //     Status = status;
        //     Notes = notes;
        // }
    }
}
