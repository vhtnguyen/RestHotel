using Hotel.DataAccess.Entities;
using Hotel.DataAccess.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DataAccess.Repositories
{
    internal class RoomDetailRepository : IRoomDetailRepository
    {
        private readonly IGenericRepository<RoomDetail> _genericRepository;

        public RoomDetailRepository(IGenericRepository<RoomDetail> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public Task<IEnumerable<RoomDetail>> BrowserAsync()
        {
            return _genericRepository.BrowserAsync();
        }

        public async Task CreateAsync(RoomDetail entity)
        {
                await _genericRepository.CreateAsync(entity);
            await _genericRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync( RoomDetail entity)
        {
            await _genericRepository.DeleteAsync(entity);
            await _genericRepository.SaveChangesAsync();
        }

        public async Task<RoomDetail?> FindAsync(Expression<Func<RoomDetail, bool>> predicate)
        {
            return  await _genericRepository.FindAsync(predicate);
        }

        public async Task<RoomDetail?> FindAsync(Guid id)
        {
            return await _genericRepository.FindAsync(id);
        }

        public async Task UpdateAsync(RoomDetail entity)
        {
             await _genericRepository.UpdateAsync(entity);

        }
    }
}
