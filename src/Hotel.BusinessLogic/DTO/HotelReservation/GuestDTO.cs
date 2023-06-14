using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.DTO.HotelReservation
{
    public class GuestDTO
    {
        public string? Name { get; set; }
        public string? TelephoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Type { get; set; }
        public string? PersonIdentification { get; set; }

        public GuestDTO(string? name, string? telephoneNumber, string? address, string? type, string? personIdentification)
        {
            Name = name;
            TelephoneNumber = telephoneNumber;
            Address = address;
            Type = type;
            PersonIdentification = personIdentification;
        }
    }
}
