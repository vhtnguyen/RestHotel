using Hotel.DataAccess.Context;
using Hotel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using org.apache.zookeeper.data;
using Hotel.DataAccess.Repositories;

namespace Hotel.DataAccess.Repositories;

internal class HotelServiceRepository : IHotelServiceRepository
{
    private readonly IGenericRepository<HotelService> _genericRepository;
    private readonly AppDbContext _context;

    public HotelServiceRepository(
        IGenericRepository<HotelService> genericRepository, AppDbContext context)
    {
        _genericRepository = genericRepository;
        _context = context;
    }

   

    public async Task<List<HotelService>?> GetListAsync()
    {
        var result = await _context.HotelService
           .Include(u => u.Category)
           .ToListAsync();
        return result;
    }


    ////// some delegate method

    
}

