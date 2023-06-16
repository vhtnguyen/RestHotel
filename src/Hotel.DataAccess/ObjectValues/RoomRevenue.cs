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


        public RoomRevenue(int id, int freq, double price)
        {
            this.id = id;
            this.freq = freq;
            caculateTotalSum(price);
        }
        public void caculateTotalSum(double price)
        {
            this.totalSum = price * freq;

        }
          public int id { get; set; }
         public int freq { get; set; }
        public double totalSum { get; set; }

    }
}
