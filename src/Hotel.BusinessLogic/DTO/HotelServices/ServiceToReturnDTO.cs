using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.DTO.HotelServices
{
    public class ServiceToReturnDTO
    {
        public int Id { get; set; }
        public string? ServiceName { get; set; }
        public string? Category { get; set; }
        public double UnitPrice { get; set; }
        //public string? Image { get; set; }
    }
}
