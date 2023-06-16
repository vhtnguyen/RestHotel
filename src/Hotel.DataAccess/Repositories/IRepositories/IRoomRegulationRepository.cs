using System.Linq.Expressions;
using Hotel.DataAccess.Entities;
namespace Hotel.DataAccess.Repositories.IRepositories
{
    public interface IRoomRegulationRepository
    {
        Task<IEnumerable<RoomRegulation>> BrowserAsync();
        Task<RoomRegulation?> FindAsync(Expression<Func<RoomRegulation, bool>> predicate);
        Task<RoomRegulation?> FindAsync(int id);
        Task CreateAsync(RoomRegulation entity);
        Task UpdateAsync(RoomRegulation entity);
        Task DeleteAsync(int id, RoomRegulation entity);
    }
}
