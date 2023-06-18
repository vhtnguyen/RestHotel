using Hotel.BusinessLogic.DTO.RoomRevenue;
using Hotel.DataAccess.ObjectValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.Services.IServices
{
    public interface IRoomRevenueService
    {

        public Task<IEnumerable<RoomRevenueToReturnDTO>> getAll();
        public Task<IEnumerable<RoomRevenueToReturnDTO>> getByMonth(int month, int year);
    }
}
