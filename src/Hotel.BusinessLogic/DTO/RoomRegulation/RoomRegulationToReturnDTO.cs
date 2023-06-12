using Hotel.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.DTO.RoomRegulation
{
    public  class RoomRegulationToReturnDTO
    {
        public int Id { get; set; }
        public int MaxGuest { get; set; }
        public int DefaultGuest { get; set; }
        public double MaxSurchargeRatio { get; set; }
        public double MaxOverseaSurchargeRatio { get; set; }
        public double RoomExchangeFee { get; set; }


        public RoomRegulationToReturnDTO(
            int id, int maxGuest, int defaultGuest, double maxSurchargeRatio, double maxOverseaSurchargeRatio, double roomExchangeFee)
        {
            Id = id;
            MaxGuest = maxGuest;
            DefaultGuest = defaultGuest;
            MaxSurchargeRatio = maxSurchargeRatio;
            MaxOverseaSurchargeRatio = maxOverseaSurchargeRatio;
            RoomExchangeFee = roomExchangeFee;
        }
    }
}
