using Hotel.DataAccess.Entities;
namespace Hotel.DataAccess.Repositories.IRepositories;

public interface IServiceCategoryRepository
{
    Task<ServiceCategory?> FindAsync(int id);
}
