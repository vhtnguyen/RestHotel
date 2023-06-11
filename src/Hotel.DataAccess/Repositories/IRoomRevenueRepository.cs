using Hotel.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DataAccess.Repositories
{
    public interface IRoomRevenueRepository
    {
        Task<IEnumerable<RoomRegulation>> BrowserAsync();
        Task<RoomRegulation?> FindAsync(Expression<Func<RoomRegulation, bool>> predicate);
    }
}
