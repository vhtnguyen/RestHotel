using Hotel.BusinessLogic.DTO.RoomDetail;
using Hotel.BusinessLogic.DTO.RoomRegulation;
using Hotel.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.Services.IServices
{
    public interface IRoomDetailService
    {
        Task<IEnumerable<RoomDetailToReturnDTO>> getAllRoomDetail();
        Task<RoomDetailToReturnDTO> getRoomDetailByID(int id);
        Task AddRoomDetail(RoomDetailToCreateDTO roomRegulation);
        Task RemoveRoomDetail(int id);
        Task UpdateRoomDetail(RoomDetail regulation);
    }
}
