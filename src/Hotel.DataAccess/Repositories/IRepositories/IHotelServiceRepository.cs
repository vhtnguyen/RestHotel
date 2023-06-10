using Hotel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Hotel.DataAccess.Repositories;

public interface IHotelServiceRepository
{
    Task<List<HotelService>?> GetListAsync();
    Task<HotelService?> CreateAsync(HotelService service, int categoryId);
    Task RemoveAsync(int id);

}
