using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.BusinessLogic.DTO.RoomRegulation;
using Hotel.DataAccess.Entities;
using Hotel.DataAccess.Repositories;
namespace Hotel.BusinessLogic.Services
{
    public interface IRoomRegulationService
    {
        Task<IEnumerable<RoomRegulationToReturnDTO>> getAllRoomRegulation();
        Task<RoomRegulationToReturnDTO> getRoomByID(int id);
        Task AddRoomRegulation(RoomRegulationToCreateDTO roomRegulation);
        Task RemoveRoomRegulation(int id);
        Task UpdateRoomRegulation(RoomRegulation regulation);


    }
}
