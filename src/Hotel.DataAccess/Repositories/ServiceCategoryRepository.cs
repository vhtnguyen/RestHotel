using Hotel.DataAccess.Context;
using Hotel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Hotel.DataAccess.Repositories.IRepositories;

namespace Hotel.DataAccess.Repositories;

internal class ServiceCategoryRepository : IServiceCategoryRepository
{
    private readonly IGenericRepository<ServiceCategory> _genericRepository;
    private readonly AppDbContext _context;

      

        public ServiceCategoryRepository(
        IGenericRepository<ServiceCategory> genericRepository, AppDbContext context)
        {
             _genericRepository = genericRepository;
            _context = context;
        }


    ////// some delegate method


    public Task<ServiceCategory?> FindAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceCategory?> FindAsync(string catName)
    {
       var result=  await _context.ServiceCategory.Where(c => c.Name!.ToLower() == catName.ToLower()).FirstOrDefaultAsync();
        return result;
    }

}

