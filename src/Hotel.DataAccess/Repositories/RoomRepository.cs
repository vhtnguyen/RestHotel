using Hotel.DataAccess.Context;
using Hotel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Hotel.DataAccess.Repositories.IRepositories;

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
    public async Task<int> CountAsync()
    {
        return await _context.Room.CountAsync();
    }
    public async Task<IEnumerable<Room>?> GetListAsync(int page,int perPage)
    {
        var result = await _context.Room.Skip((page - 1) * perPage).Take(perPage)
            .Include(r => r.RoomDetail)
            .ThenInclude(rD => rD.RoomRegulation)
           .ToListAsync();
        return result;
    }

    public async Task<IEnumerable<Room>?> FindAllAsync(Expression<Func<Room, bool>> predicate)
    {
        return await _context.Room.Include(r => r.RoomDetail)
                    .ThenInclude(rD => rD.RoomRegulation).Where(predicate).ToListAsync();
    }
    public async Task<Room?> FindAsync(int id)
    {
        return await _context.Room
           .Include(r => r.RoomDetail)
            .ThenInclude(rD => rD.RoomRegulation).Where(r => r.Id == id).FirstOrDefaultAsync();
    }
    public async Task<Room?> FindAsync(Expression<Func<Room, bool>> predicate)
    {
        return await _context.Room
                    .Include(r => r.RoomDetail)
                    .ThenInclude(rD => rD.RoomRegulation)
                    .Where(predicate)
                   .FirstOrDefaultAsync();
    }
    public async Task<Room> CreateAsync(Room entity)
        => await _genericRepository.CreateAsync(entity);
    public async Task<Room> UpdateAsync(Room entity)
    {
        var room = _context.Room.Update(entity);
        await _context.SaveChangesAsync();
        return room.Entity;
    }
    public async Task RemoveAsync(Room entity)
    {
        _context.Room.Remove(entity);
        await _context.SaveChangesAsync();
    }
    public async Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
    public async Task<IEnumerable<Room>?> FindFreeByDateAsync(Expression<Func<Room, bool>> predicate)
    {
        return await _context.Room
                    .Include(r => r.RoomDetail)
                    .ThenInclude(rD => rD.RoomRegulation)
                    .Where(predicate)
                    .ToListAsync();
    }

   
}

