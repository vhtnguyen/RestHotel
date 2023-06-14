using Hotel.DataAccess.Entities;
using Hotel.DataAccess.Repositories.IRepositories;

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

