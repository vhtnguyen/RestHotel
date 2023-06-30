using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.DTO.HotelServices
{
    public class ServiceToCreateDTO
    {
        public string? ServiceName { get; set; }
        public required string Category { get; set; }
        public double UnitPrice { get; set; }
    }
}