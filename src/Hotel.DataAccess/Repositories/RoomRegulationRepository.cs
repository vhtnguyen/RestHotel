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

        public Task CreateAsync(RoomRegulation entity)
        {
              return _genericRepository.CreateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _genericRepository.FindAsync(x=>x.Id == id);
            await _genericRepository.DeleteAsync(entity);

        }

        public Task<RoomRegulation?> FindAsync(Expression<Func<RoomRegulation, bool>> predicate)
        {
            return _genericRepository.FindAsync(predicate);
        }

        public Task<RoomRegulation?> FindAsync(Guid id)
        {
            return _genericRepository.FindAsync(id);
        }

        public Task UpdateAsync(RoomRegulation entity)
        {
           return _genericRepository.UpdateAsync(entity);
        }
    }
}
