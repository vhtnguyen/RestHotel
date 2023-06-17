using Hotel.BusinessLogic.DTO.RoomRevenue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.DTO.RoomOccupancy
{
    public class RoomOccupancyToReturnDTO
    {
        public RoomOccupancyToReturnDTO(int id, int freq)
        {
            this.id = id;
            this.freq = freq;
            this.percentage = 0;
        }

        public int id { get; set; }
        public int freq { get; set; }
        public double percentage { get; set; }
        public void getPercentage(List<RoomOccupancyToReturnDTO> src)
        {
            double totalFreq = 0;
            foreach (var roomType in src)
            {
                totalFreq += roomType.freq;
            }
            this.percentage = this.freq * 100 / totalFreq;
        }

    }
}
