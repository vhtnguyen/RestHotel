using Hotel.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.DTO.RoomRegulation
{
    public class RoomRegulationToCreateDTO
    {
      
        public int MaxGuest { get; set; }
        public int DefaultGuest { get; set; }
        public double MaxSurchargeRatio { get; set; }
        public double MaxOverseaSurchargeRatio { get; set; }
        public double RoomExchangeFee { get; set; }

        public ICollection<RoomRegulationRoomDetail> RoomDetails { get; set; } = new List<RoomRegulationRoomDetail>();
        [JsonConstructor]
        public RoomRegulationToCreateDTO(
            int maxGuest, int defaultGuest, double maxSurchargeRatio, double maxOverseaSurchargeRatio, double roomExchangeFee)
        {
          
            MaxGuest = maxGuest;
            DefaultGuest = defaultGuest;
            MaxSurchargeRatio = maxSurchargeRatio;
            MaxOverseaSurchargeRatio = maxOverseaSurchargeRatio;
            RoomExchangeFee = roomExchangeFee;
        }
    }
}
