using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Hotel.DataAccess.Entities;
namespace Hotel.DataAccess.Repositories
{
    internal class RoomRegulationRepository: IRoomRegulationRepository
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

        public async Task  CreateAsync(RoomRegulation entity)
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

        public async Task<RoomRegulation?> FindAsync(Guid id)
        {
            return await _genericRepository.FindAsync(id);
        }

        public Task UpdateAsync(RoomRegulation entity)
        {
           return _genericRepository.UpdateAsync(entity);
        }
    }
}
