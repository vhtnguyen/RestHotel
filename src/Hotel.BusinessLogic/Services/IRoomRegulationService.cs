using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.DataAccess.Entities;
using Hotel.DataAccess.Repositories;
namespace Hotel.BusinessLogic.Services
{
    public interface IRoomRegulationService
    {
        Task<IEnumerable<RoomRegulation>> getAllRoomRegulation();

        void AddRoomRegulation(RoomRegulation regulation);
        void RemoveRoomRegulation(RoomRegulation regulation);
        void UpdateRoomRegulation(RoomRegulation regulation);


    }
}
