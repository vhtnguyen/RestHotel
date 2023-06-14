using Hotel.DataAccess.Entities;
using System.Linq.Expressions;

namespace Hotel.DataAccess.Repositories.IRepositories;

public interface IHotelServiceRepository
{
    Task<IEnumerable<HotelService>?> GetListAsync();
    Task<HotelService?> CreateAsync(HotelService service, int categoryId);
    Task<IEnumerable<HotelService>?> FindAllAsync(Expression<Func<HotelService, bool>> predicate);
    Task RemoveAsync(int id);

}
