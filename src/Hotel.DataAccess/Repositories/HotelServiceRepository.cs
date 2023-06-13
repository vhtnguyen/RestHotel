using Hotel.DataAccess.Context;
using Hotel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using org.apache.zookeeper.data;
using Hotel.DataAccess.Repositories;
using Stripe;

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

   

    public async Task<IEnumerable<HotelService>?> GetListAsync()
    {
        var result = await _context.HotelService
           .Include(u => u.Category)
           .ToListAsync();
        return result;
    }


    /// some delegate method
    

    public async Task<HotelService?> CreateAsync(HotelService service,int categoryId)
    {
        var category = await _context.ServiceCategory
            .SingleOrDefaultAsync(s => s.Id == categoryId);


        if(category != null) 
        {
            service.Category = category;
            category.HotelServices.Add(service);
             _context.SaveChanges();
            return   service;

        }
        else { return null; }
           

    }

    public async Task RemoveAsync(int id)
    {
        var service_to_remove = await _context.HotelService.FirstOrDefaultAsync(s => s.Id == id);
        if (service_to_remove != null)
        {
            _context.HotelService.Remove(service_to_remove);
            await _context.SaveChangesAsync();
        }
        else
        {

        }
    }

    public async Task<IEnumerable<HotelService>?> FindAllAsync(Expression<Func<HotelService, bool>> predicate)
    {
        return await _genericRepository.FindAllAsync(predicate);
    }


}

