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
            var today= DateTime.Now;
            var month=today.Month;
            var year=today.Year;
            var daysOfMonth = DateTime.DaysInMonth(year, month);
            var monthStart = new DateTime(year, month, 1);
            var monthEnd = new DateTime(year, month, 1).AddMonths(1).AddDays(-1);

            var joinTable = from r in _dbContext.Room
                            join rc in _dbContext.ReservationCard on r.Id equals rc.Room.Id
                            join rd in _dbContext.RoomDetail on r.RoomDetail.Id equals rd.Id
                            where rc.ArrivalDate <= monthEnd && rc.DepartureDate>= monthStart  
                            select new { rcId = rc.Id, rdId = rd.Id, rId = r.Id, price = rd.Price, totalDate = -(rc.ArrivalDate < monthStart ? monthStart.Day : rc.ArrivalDate.Day) + (rc.DepartureDate > monthEnd ? monthEnd.Day : rc.DepartureDate.Day) +1 };

            //Console.Write("revenue");
            var res = joinTable.GroupBy(x => new { id = x.rdId, price = x.price }).Select(y => new { rdId = y.Key.id, price = y.Key.price, totalDate = y.Sum(x => x.totalDate) });
            var res2 = res.ToArray();
            List<RoomRevenue> roomRevenueList = new List<RoomRevenue>();
            foreach (var x in res2)
            {
                RoomRevenue roomRevenue = new RoomRevenue(x.rdId, x.totalDate, x.price);
                //Console.WriteLine(x);
                //Console.WriteLine(roomRevenue.id + "+" + roomRevenue.freq + "+ " );

                roomRevenueList.Add(roomRevenue);
            }

            return roomRevenueList;

        }
        public async Task<IEnumerable<RoomRevenue>> FindAsync(int month, int year)
        {

            var days = DateTime.DaysInMonth(year, month);
            var monthStart = new DateTime(year, month, 1);
            var monthEnd= new DateTime(year, month, 1).AddMonths(1).AddDays(-1);
            var joinTable = from r in _dbContext.Room
                      join rc in _dbContext.ReservationCard on r.Id equals rc.Room.Id
                      join rd in _dbContext.RoomDetail on r.RoomDetail.Id equals rd.Id
                      where rc.ArrivalDate <= monthEnd && rc.DepartureDate >= monthStart
                      select new { rcId=rc.Id,  rdId = rd.Id, rId = r.Id, price = rd.Price, totalDate = -(rc.ArrivalDate<monthStart?monthStart.Day:rc.ArrivalDate.Day) + (rc.DepartureDate>monthEnd?monthEnd.Day:rc.DepartureDate.Day) + 1 };

            var res = joinTable.GroupBy(x => new { id = x.rdId, price = x.price }).Select(y => new { rdId = y.Key.id, price = y.Key.price, totalDate = y.Sum(x => x.totalDate) });

            var res2 =  res.ToArray();
            List<RoomRevenue> roomRevenueList = new List<RoomRevenue>();
            foreach (var x in res2)
            {
                RoomRevenue roomRevenue = new RoomRevenue(x.rdId, x.totalDate, x.price);
                //Console.WriteLine(roomRevenue.id+"+"+ roomRevenue.freq + "+ " +x.totalDate);
                
                //Console.WriteLine(x);
                roomRevenueList.Add(roomRevenue);
            }

            return roomRevenueList;
        }


    }
}
