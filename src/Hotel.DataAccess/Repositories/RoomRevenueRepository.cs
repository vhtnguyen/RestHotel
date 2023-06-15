using Hotel.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DataAccess.Repositories
{
    internal class RoomRevenuRepository : IRoomRevenueRepository
    {
        public Task<IEnumerable<RoomRegulation>> BrowserAsync()
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(RoomRegulation entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id, RoomRegulation entity)
        {
            throw new NotImplementedException();
        }

        public Task<RoomRegulation?> FindAsync(Expression<Func<RoomRegulation, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<RoomRegulation?> FindAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(RoomRegulation entity)
        {
            throw new NotImplementedException();
        }
    }
}
