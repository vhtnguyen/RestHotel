using Hotel.DataAccess.Context;
using Hotel.DataAccess.ObjectValues;
using Hotel.DataAccess.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DataAccess.Repositories
{
    internal class RoomOccupancyRepository : IRoomOccupancyRepository
    {
        private readonly AppDbContext _dbContext;

        public RoomOccupancyRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<RoomOccupancy>> BrowserAsync()
        {
            var res = from r in _dbContext.Room
                      join rc in _dbContext.ReservationCard on r.Id equals rc.Room.Id
                      select new { rcId = rc.Id,  rId = r.Id } into x
                      group x by new { x.rId } into y
                      select new { id = y.Key.rId, freq = y.Sum(y => y.rId)};
            //Console.Write("revenue");
            var res2 = res.ToArray();
            List<RoomOccupancy> roomRevenueList = new List<RoomOccupancy>();
            foreach (var x in res2)
            {
                RoomOccupancy roomRevenue = new RoomOccupancy(x.id, x.freq);
                //Console.WriteLine(x);
                roomRevenueList.Add(roomRevenue);
            }

            return roomRevenueList;
        }

        public Task<RoomOccupancy?> FindAsync(Expression<Func<RoomOccupancy, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
