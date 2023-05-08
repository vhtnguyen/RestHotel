using Hotel.DataAccess.Entities;
using System.Linq.Expressions;

namespace Hotel.DataAccess.Repositories;

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
}
