using Hotel.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Hotel.DataAccess.ObjectValues;
namespace Hotel.DataAccess.Repositories.IRepositories
{
    public interface IRoomRevenueRepository
    {
        Task <IEnumerable<RoomRevenue>> BrowserAsync();
        Task<RoomRegulation?> FindAsync(Expression<Func<RoomRegulation, bool>> predicate);
    }
}
