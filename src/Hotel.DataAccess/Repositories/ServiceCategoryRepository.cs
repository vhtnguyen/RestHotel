using Hotel.DataAccess.Context;
using Hotel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using org.apache.zookeeper.data;
using Hotel.DataAccess.Repositories;

namespace Hotel.DataAccess.Repositories;

internal class ServiceCategoryRepository : IServiceCategoryRepository
{
    private readonly IGenericRepository<ServiceCategory> _genericRepository;


    public ServiceCategoryRepository(
        IGenericRepository<ServiceCategory> genericRepository)
    {
        _genericRepository = genericRepository;
    }


    ////// some delegate method


    public Task<ServiceCategory?> FindAsync(int id)
    {
        throw new NotImplementedException();
    }
 
}

