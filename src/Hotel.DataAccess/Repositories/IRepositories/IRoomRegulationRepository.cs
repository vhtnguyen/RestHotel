using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Hotel.DataAccess.Entities;
namespace Hotel.DataAccess.Repositories
{
    public interface IRoomRegulationRepository
    {
        Task<IEnumerable<RoomRegulation>> BrowserAsync();
        Task<RoomRegulation?> FindAsync(Expression<Func<RoomRegulation, bool>> predicate);
        Task<RoomRegulation?> FindAsync(Guid id);
        Task CreateAsync(RoomRegulation entity);
        Task UpdateAsync(RoomRegulation entity);
        Task DeleteAsync(int id);
    }
}
