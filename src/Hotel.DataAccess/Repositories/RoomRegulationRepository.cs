
using System.Linq.Expressions;
using Hotel.DataAccess.Entities;
using Hotel.DataAccess.Repositories.IRepositories;

namespace Hotel.DataAccess.Repositories
{
    internal class RoomRegulationRepository : IRoomRegulationRepository
    {
        private readonly IGenericRepository<RoomRegulation> _genericRepository;

        public RoomRegulationRepository(IGenericRepository<RoomRegulation> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public Task<IEnumerable<RoomRegulation>> BrowserAsync()
        {
            return _genericRepository.BrowserAsync();
        }

        public async Task CreateAsync(RoomRegulation entity)
        {
            await _genericRepository.CreateAsync(entity);
            await _genericRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, RoomRegulation entity)
        {
            await _genericRepository.DeleteAsync(entity);
            await _genericRepository.SaveChangesAsync();

        }

        public async Task<RoomRegulation?> FindAsync(Expression<Func<RoomRegulation, bool>> predicate)
        {
            return await _genericRepository.FindAsync(predicate);
        }

        public async Task<RoomRegulation?> FindAsync(int id)
        {
            return await _genericRepository.FindAsync(id);
        }

        public Task UpdateAsync(RoomRegulation entity)
        {
            return _genericRepository.UpdateAsync(entity);
        }
    }
}
