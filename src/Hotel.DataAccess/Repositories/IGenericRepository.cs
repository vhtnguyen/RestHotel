using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Hotel.DataAccess.Repositories;

//https://learn.microsoft.com/en-us/ef/core/saving/transactions
internal interface IGenericRepository<TEntity>
    where TEntity : class
{
    Task<IEnumerable<TEntity>> BrowserAsync();
    Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> FindAsync(Guid id);
    Task CreateAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task SaveChangesAsync();
    Task<IDbContextTransaction> CreateTransaction();
}
