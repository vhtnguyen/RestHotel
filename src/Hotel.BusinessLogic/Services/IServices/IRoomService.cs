using Hotel.BusinessLogic.DTO.Rooms;
using Hotel.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.Services
{
    public interface IRoomService
    {
        Task<RoomToReturnQueryPerPageDTO> GetRoomListAsync(int page,int pageSize);
        Task<RoomToReturnDetailDTO> CreateRoomAsync(RoomToCreateDTO roomToCreateDTO);
        Task<RoomToReturnDetailDTO> GetRoomByIDAsync(int id);
        Task<List<RoomToReturnDetailDTO>> FindRooms(string roomType, string status);
        Task<RoomToReturnDetailDTO> UpdateRoomAsync(RoomToCreateDTO room);
        Task RemoveRoomByIDAsync(int id);
        Task<List<RoomFreeToReturnDTO>?> FindFreeByDateAsync(RoomToFindFreeDTO roomToFindFreelDTO);
    }
}
