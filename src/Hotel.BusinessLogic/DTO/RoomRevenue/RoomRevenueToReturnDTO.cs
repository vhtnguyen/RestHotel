using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.DTO.RoomRevenue
{
    public class RoomRevenueToReturnDTO
    {
        public RoomRevenueToReturnDTO(double percentage)
        {
            this.percentage = percentage;
        }

        public RoomRevenueToReturnDTO(int id, double totalSum)
        {
            this.id = id;
            this.totalSum = totalSum;
         
        }

       public int id { get; set; }
     
    public    double totalSum { get; set; }

public        double percentage { get; set; }
        public void getPercentage( List<RoomRevenueToReturnDTO> src)
        {
            double totalSum = 0;
            foreach(var roomType in src)
            {
               totalSum += roomType.totalSum; 
            }
            this.percentage = this.totalSum*100 / totalSum;
        }
    }
}
