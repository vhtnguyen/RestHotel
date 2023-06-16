using Hotel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Hotel.DataAccess.Repositories.IRepositories;

public interface IRoomRepository
{
    Task<IEnumerable<Room>?> GetListAsync();
    Task<Room?> FindAsync(Expression<Func<Room, bool>> predicate);
    Task<IEnumerable<Room>?> FindAllAsync(Expression<Func<Room, bool>> predicate);
    Task<Room?> FindAsync(int id);
    Task<Room> CreateAsync(Room entity);
    Task UpdateAsync(Room entity);
    Task RemoveByIDAsync(int id);
    Task SaveChangesAsync();
    Task<IEnumerable<Room>?> FindFreeByDateAsync(Expression<Func<Room, bool>> predicate);
}