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
        public RoomRevenueToReturnDTO(int id, int freq, double price)
        {
            this.id = id;
            this.freq = freq;
             caculateTotalSum(price);
        }
        public void caculateTotalSum(double price)
        {
            this.totalSum=price*freq;

        }
        int id { get; set; }
        int freq { get; set; }
        double totalSum { get; set; }
    }
}
