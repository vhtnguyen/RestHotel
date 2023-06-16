using Hotel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Hotel.DataAccess.Repositories.IRepositories;

public interface IRoleRepository
{
    Task<Role?> FindAsync(Expression<Func<Role, bool>> predicate);
    Task<List<Role>?> FindAllAsync(Expression<Func<Role, bool>> predicate);


}
