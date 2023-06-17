using Hotel.BusinessLogic.DTO.RoomRegulation;
using Hotel.DataAccess.Entities;
namespace Hotel.BusinessLogic.Services.IServices
{
    public interface IRoomRegulationService
    {
        Task<IEnumerable<RoomRegulationToReturnDTO>> getAllRoomRegulation();
        Task<RoomRegulationToReturnDTO> getRoomByID(int id);
        Task AddRoomRegulation(RoomRegulationToCreateDTO roomRegulation);
        Task RemoveRoomRegulation(int id);
        Task UpdateRoomRegulation(int id, RoomRegulationToCreateDTO regulation);


    }
}
