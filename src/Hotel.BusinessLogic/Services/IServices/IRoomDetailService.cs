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
        Task <RoomDetailToReturnDTO> CreateRoomDetail(RoomDetailToCreateDTO roomDetail);
        Task RemoveRoomDetail(int id);
        Task UpdateRoomDetail(RoomDetailToReturnDTO regulation);
        Task<IEnumerable<int>> getAllId();

    }
}
