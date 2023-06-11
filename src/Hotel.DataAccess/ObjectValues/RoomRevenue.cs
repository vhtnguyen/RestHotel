using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.DataAccess.Entities;
namespace Hotel.DataAccess.ObjectValues
{
    public class RoomRevenue
    {
     

        public RoomRevenue(ICollection<Room> roomLists)

        {
            //roomLists.Any(x=>x.)
        }

        public int Id { get; set; }
        public string RoomType { get; set; }
        public double TotalRevenue { get; set; }
    }
}
