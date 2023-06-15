using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Hotel.DataAccess.Repositories.IRepositories;

public interface IGenericRepository<TEntity>
    where TEntity : class
{

    Task<List<TEntity>> GetListAsync();
    Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> BrowserAsync();
    Task<TEntity?> FindAsyncById(int id);
    Task<TEntity?> FindAsync(Guid id);
    Task<TEntity> CreateAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task<IEnumerable<TEntity>?> FindAllAsync(Expression<Func<TEntity, bool>> predicate);
    Task SaveChangesAsync();
    //https://learn.microsoft.com/en-us/ef/core/saving/transactions
    Task<IDbContextTransaction> CreateTransaction();
}
