using Hotel.DataAccess.Entities;
using System.Linq.Expressions;

namespace Hotel.DataAccess.Repositories.IRepositories;

public interface IServiceCategoryRepository
{
    Task<ServiceCategory?> FindAsync(int id);
    Task<ServiceCategory?> FindAsync(string catName);
}
