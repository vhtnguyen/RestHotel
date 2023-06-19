using Hotel.BusinessLogic.DTO.RoomOccupancy;


namespace Hotel.BusinessLogic.Services.IServices
{
    public interface IRoomOccupancyService
    {
        public Task<IEnumerable<RoomOccupancyToReturnDTO>> getAll();
        public Task<IEnumerable<RoomOccupancyToReturnDTO>> getByTypeAndMonth(int id, int month, int year);
        public Task<IEnumerable<RoomOccupancyToReturnDTO>> getByRoomDetailId(int id);
        public Task<IEnumerable<RoomOccupancyToReturnDTO>> getByMonth(int month,int year);

    }
}
