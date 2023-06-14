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
        Task<List<RoomToReturnListDTO>> GetRoomListAsync();
        Task<RoomToReturnDetailDTO> CreateRoomAsync(RoomToCreateDTO roomToCreateDTO);
        //Task<IEnumerable<RoomToReturnListDTO>> SearchUserAsync(string searchOption, string searchContent);
        //Task RemoveRoomAsync(int roomId);
    }
}
