using Hotel.DataAccess.Context;
using Hotel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using org.apache.zookeeper.data;
using Hotel.DataAccess.Repositories;
using System.Linq;
using System;

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

    public async Task<IEnumerable<Room>?> FindAllAsync(Expression<Func<Room, bool>> predicate)
    {
        return await _context.Room.Include(r=>r.RoomDetail).Where(predicate).ToListAsync();
    }
    public async Task<Room?> FindAsync(int id)
    {
        return await _context.Room
           .Include(r => r.RoomDetail).Where(r => r.Id == id).FirstOrDefaultAsync();
    }
    public async Task<Room?> FindAsync(Expression<Func<Room, bool>> predicate)
    {
        throw new NotImplementedException();
    }
    public async Task<Room> CreateAsync(Room entity)
    => await _genericRepository.CreateAsync(entity);
    public async Task UpdateAsync(Room entity)
    {
        throw new NotImplementedException();
    }
    public async Task RemoveByIDAsync(int id)
    {
        var room_to_remove = await _context.Room.FirstOrDefaultAsync(r=>r.Id==id);
        if (room_to_remove == null)
        {
            throw new NotImplementedException();
        }
        else
        {
            _context.Room.Remove(room_to_remove);
            await _context.SaveChangesAsync();
        }
    }
    public async Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
    public async Task<IEnumerable<Room>?> FindFreeByDateAsync(Expression<Func<Room, bool>> predicate)
    {
        return await _context.Room
                    .Include(r=>r.RoomDetail)
                    .ThenInclude(rD=>rD.RoomRegulation)
                    .Where(predicate)
                    .ToListAsync();
    }
}

