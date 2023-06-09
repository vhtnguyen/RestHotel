using Hotel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Hotel.DataAccess.Repositories;

public interface IHotelServiceRepository
{
    Task<List<HotelService>?> GetListAsync();
   
}
