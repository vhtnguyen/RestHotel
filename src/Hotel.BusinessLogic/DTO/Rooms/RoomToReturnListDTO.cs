using Hotel.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.DTO.Rooms
{
    public class RoomToReturnListDTO
    {
        //from room
        public int Id { get; set; }
        public string? Note { get; set; }
        public string? Status { get; set; }
        //from roomdetail
        public int RoomDetail { get; set; }
        public string? RoomType { get; set; }
        public double Price { get; set; }
        
    }
}
