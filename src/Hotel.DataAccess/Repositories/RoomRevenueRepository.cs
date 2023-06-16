using Hotel.DataAccess.Context;
using Hotel.DataAccess.Entities;
using Hotel.DataAccess.ObjectValues;
using Hotel.DataAccess.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DataAccess.Repositories
{
    internal class RoomRevenuRepository : IRoomRevenueRepository
    {

        private readonly AppDbContext _dbContext;

        public RoomRevenuRepository(AppDbContext dbContext)
        {

            _dbContext = dbContext;
        }

        public async Task<IEnumerable<RoomRevenue>> BrowserAsync()
        {
            var res = from r in _dbContext.Room
                      join rc in _dbContext.ReservationCard on r.Id equals rc.Room.Id
                      join rd in _dbContext.roomDetails on r.RoomDetail.Id equals rd.Id
                      select new { rcId = rc.Id, rdId = rd.Id, rId = r.Id, price = rd.Price } into x
                      group x by new { x.rdId, x.price } into y
                      select new { id = y.Key.rdId, freq = y.Sum(y => y.rdId), price = y.Key.price };
            //Console.Write("revenue");
            var res2 = res.ToArray();
            List<RoomRevenue> roomRevenueList = new List<RoomRevenue>();
            foreach (var x in res2)
            {
                RoomRevenue roomRevenue = new RoomRevenue(x.id, x.freq, x.price);
                //Console.WriteLine(x);
                roomRevenueList.Add(roomRevenue);
            }

            return roomRevenueList;

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
