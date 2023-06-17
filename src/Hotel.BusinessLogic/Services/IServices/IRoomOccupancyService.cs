using Hotel.BusinessLogic.DTO.RoomOccupancy;
using Hotel.BusinessLogic.DTO.RoomRevenue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.Services.IServices
{
    public interface IRoomOccupancyService
    {
        public Task<IEnumerable<RoomOccupancyToReturnDTO>> getAll();
    }
}
