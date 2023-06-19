using Hotel.DataAccess.Context;
using Hotel.DataAccess.Entities;
using Hotel.DataAccess.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Hotel.DataAccess.Repositories
{
    internal class RoomDetailRepository : IRoomDetailRepository
    {
        private readonly IGenericRepository<RoomDetail> _genericRepository;
        private readonly AppDbContext _context;

        public RoomDetailRepository(IGenericRepository<RoomDetail> genericRepository, AppDbContext context)
        {
            _genericRepository = genericRepository;
            _context = context;
        }

        public async Task<IEnumerable<RoomDetail>> BrowserAsync()
        {
            var result = await _context.roomDetails.Include(x => x.RoomRegulation).ToListAsync();

            return result;
        }

        public async Task CreateAsync(RoomDetail entity)
        {
            await _genericRepository.CreateAsync(entity);
            await _genericRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(RoomDetail entity)
        {
            await _genericRepository.DeleteAsync(entity);
            await _genericRepository.SaveChangesAsync();
        }

        public async Task<RoomDetail?> FindAsync(Expression<Func<RoomDetail, bool>> predicate)
        {
            return await _context.roomDetails.Include(i => i.RoomRegulation).FirstOrDefaultAsync(predicate);
        }

        public async Task<RoomDetail?> FindAsync(int id)
        {
            return await _genericRepository.FindAsync(id);
        }

        public async Task<IEnumerable<int>> GetAllId()
        {
            var query = from rd in _context.roomDetails
                        select new { id = rd.Id };

            var res = await query.ToArrayAsync();
            List<int> result = new List<int>();
            foreach (var id in res)
            {
                result.Add(id.id);
            }

            return result;
        }

        public async Task SaveChangeAsync()
        {
            await _genericRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(RoomDetail entity)
        {
            await _genericRepository.UpdateAsync(entity);


        }

    }
}
