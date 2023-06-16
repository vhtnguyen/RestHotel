using Hotel.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Hotel.BusinessLogic.DTO.Rooms
{
    public class RoomFreeToReturnDTO
    {
        public int Id { get; set;}
        public string? Status { get; set; }
        public string? Note { get; set; }
        public string? Description {get; set;}
        public int DefaultGuest {get; set;}
        public int MaxGuest {get; set;}
        public double Price {get; set;}
    }
}
