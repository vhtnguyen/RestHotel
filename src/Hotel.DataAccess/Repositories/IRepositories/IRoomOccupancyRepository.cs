using Hotel.DataAccess.Entities;
using Hotel.DataAccess.ObjectValues;


namespace Hotel.DataAccess.Repositories.IRepositories
{
    public interface IRoomOccupancyRepository
    {
        Task<IEnumerable<RoomOccupancy>> BrowserAsync();
        Task<IEnumerable<RoomOccupancy>> FindByAllFilters(int id, int month, int year);

        Task<IEnumerable<RoomOccupancy>> FindByRoomDetailFilters(int id);

    }
}
