using Hotel.DataAccess.Entities;
using System.Linq.Expressions;

namespace Hotel.DataAccess.Repositories.IRepositories
{
    public interface IRoomRevenueRepository
    {
        Task<IEnumerable<RoomRegulation>> BrowserAsync();
        Task<RoomRegulation?> FindAsync(Expression<Func<RoomRegulation, bool>> predicate);
    }
}
