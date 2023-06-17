using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DataAccess.ObjectValues
{
    public class RoomOccupancy
    {
        public RoomOccupancy(int id, int freq)
        {
            this.id = id;
            this.freq = freq;
        }

        public int id { get; set; }
        public int freq { get;set; }


    }
}
