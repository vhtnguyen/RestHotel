using Hotel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Hotel.DataAccess.Repositories;

public interface IHotelServiceRepository
{
    Task<IEnumerable<HotelService>?> GetListAsync();
    Task<HotelService?> CreateAsync(HotelService service, int categoryId);
    Task<IEnumerable<HotelService>?> FindAllAsync(Expression<Func<HotelService, bool>> predicate);
    Task RemoveAsync(int id);

}
