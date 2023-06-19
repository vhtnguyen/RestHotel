using Hotel.DataAccess.Context;
using Hotel.DataAccess.ObjectValues;
using Hotel.DataAccess.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;


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
            var today = DateTime.Now;
            var month = today.Month;
            var year = today.Year;
            var daysOfMonth = DateTime.DaysInMonth(year, month);
            var monthStart = new DateTime(year, month, 1);
            var monthEnd = new DateTime(year, month, 1).AddMonths(1).AddDays(-1);

            var jointable = from r in _dbContext.Room
                      join rc in _dbContext.ReservationCard on r.Id equals rc.Room.Id
                      where rc.ArrivalDate <= monthEnd && rc.DepartureDate >= monthStart
                      //select new { rcId = rc.Id,  rId = r.Id } into x
                      //group x by new { x.rId } into y
                      select new { rcId=rc.Id, rId=r.Id,  totalDate = -(rc.ArrivalDate < monthStart ? monthStart.Day : rc.ArrivalDate.Day) + (rc.DepartureDate > monthEnd ? monthEnd.Day : rc.DepartureDate.Day) + 1 };
            //Console.Write("revenue");
            var res = jointable.GroupBy(x => new { id = x.rId }).Select(y => new { rId = y.Key.id, totalDate = y.Sum(x => x.totalDate) });

            var res2 = res.ToArray();
            List<RoomOccupancy> roomRevenueList = new List<RoomOccupancy>();
            foreach (var x in res2)
            {
                RoomOccupancy roomRevenue = new RoomOccupancy(x.rId, x.totalDate);
                //Console.WriteLine(x);
                roomRevenueList.Add(roomRevenue);
            }

            return roomRevenueList;
        }

        public async Task<IEnumerable<RoomOccupancy>> FindByAllFilters(int id, int month, int year)
        {

            var days = DateTime.DaysInMonth(year, month);
            var monthStart = new DateTime(year, month, 1);
            var monthEnd = new DateTime(year, month, 1).AddMonths(1).AddDays(-1);
            var joinTable = from r in _dbContext.Room
                            join rc in _dbContext.ReservationCard on r.Id equals rc.Room.Id
                            join rd in _dbContext.roomDetails on r.RoomDetail.Id equals rd.Id
                            where rc.ArrivalDate <= monthEnd && rc.DepartureDate >= monthStart && rd.Id == id
                            select new { rcId = rc.Id, rdId = rd.Id, rId = r.Id,  totalDate = -(rc.ArrivalDate < monthStart ? monthStart.Day : rc.ArrivalDate.Day) + (rc.DepartureDate > monthEnd ? monthEnd.Day : rc.DepartureDate.Day) + 1 };

            var res = joinTable.GroupBy(x => new { rId = x.rId}).Select(y => new { rId = y.Key.rId, totalDate = y.Sum(x => x.totalDate) });

            var res2 = res.ToArray();
            List<RoomOccupancy> roomRevenueList = new List<RoomOccupancy>();
            foreach (var x in res2)
            {
                RoomOccupancy roomRevenue = new RoomOccupancy(x.rId, x.totalDate);
                //Console.WriteLine(x);
                roomRevenueList.Add(roomRevenue);
            }

            return roomRevenueList;
        }

        public async Task<IEnumerable<RoomOccupancy>> FindByRoomDetailFilters(int id)
        {
        
            var joinTable = from r in _dbContext.Room
                            join rc in _dbContext.ReservationCard on r.Id equals rc.Room.Id
                            join rd in _dbContext.roomDetails on r.RoomDetail.Id equals rd.Id
                            where  rd.Id == id
                            select new { rId = r.Id, totalDate =  EF.Functions.DateDiffDay(rc.ArrivalDate,rc.DepartureDate ) + 1 };

            var res = joinTable.GroupBy(x => new { rId = x.rId }).Select(y => new { rId = y.Key.rId, totalDate = y.Sum(x => x.totalDate) });

            var res2 = res.ToArray();
            List<RoomOccupancy> roomRevenueList = new List<RoomOccupancy>();
            foreach (var x in res2)
            {
                RoomOccupancy roomRevenue = new RoomOccupancy(x.rId, x.totalDate);
                //Console.WriteLine("rcID" + x.rcId + "totaldate:" + x.totalDate);      
                roomRevenueList.Add(roomRevenue);
            }

            return roomRevenueList;
        }
    }
}
