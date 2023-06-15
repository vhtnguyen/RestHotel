using Hotel.DataAccess.Context;
using Hotel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using org.apache.zookeeper.data;
using Hotel.DataAccess.Repositories;
using System.Linq;

namespace Hotel.DataAccess.Repositories;

internal class RoomRepository : IRoomRepository
{
    private readonly IGenericRepository<Room> _genericRepository;
    private readonly AppDbContext _context;

    public RoomRepository(
        IGenericRepository<Room> genericRepository, AppDbContext context)
    {
        _genericRepository = genericRepository;
        _context = context;
    }

    public async Task<IEnumerable<Room>?> GetListAsync()
    {
        var result = await _context.Room
           .Include(r => r.RoomDetail)
           .ToListAsync();
        return result;
    }
    public async Task<Room?> FindAsync(Expression<Func<Room, bool>> predicate)
    {
        throw new NotImplementedException();
    }
    public async Task<IEnumerable<Room>?> FindAllAsync(Expression<Func<Room, bool>> predicate)
    {
        throw new NotImplementedException();
    }
    public async Task<Room?> FindAsync(int id)
    {
        throw new NotImplementedException();
    }
    public async Task<Room> CreateAsync(Room entity)
    => await _genericRepository.CreateAsync(entity);
    public async Task UpdateAsync(Room entity)
    {
        throw new NotImplementedException();
    }
    public async Task DeleteByIDAsync(int id)
    {
        throw new NotImplementedException();
    }
    public async Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}

