using Hotel.DataAccess.Entities;
using Hotel.DataAccess.ObjectValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DataAccess.Repositories.IRepositories
{
    public interface IRoomOccupancyRepository
    {
        Task<IEnumerable<RoomOccupancy>> BrowserAsync();
        Task<RoomOccupancy?> FindAsync(Expression<Func<RoomOccupancy, bool>> predicate);
    }
}
