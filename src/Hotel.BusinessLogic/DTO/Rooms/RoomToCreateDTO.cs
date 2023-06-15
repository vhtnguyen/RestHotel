using Hotel.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.DTO.Rooms
{
    public class RoomToCreateDTO
    {
        public int Id { get; set; }
        public string? Status { get; set; }
        public string? Note { get; set; }
        public int RoomDetailID { get; set; }
    }
}
