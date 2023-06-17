using Hotel.DataAccess.Context;
using Hotel.DataAccess.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Hotel.DataAccess.Repositories;

internal class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<TEntity> _collection;
    // pass dbcontext here
    // generate dataset
    public GenericRepository(
        AppDbContext context)
    {
        _context = context;
        _collection = _context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> BrowserAsync()
    {

        IEnumerable<TEntity> collection = await _collection.ToListAsync();
        return collection;
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        var new_entity = await _collection.AddAsync(entity);
        await _context.SaveChangesAsync();
        return new_entity.Entity;
    }
    public async Task DeleteAsync(TEntity entity)
    {
        _collection.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var entity = await _collection.SingleOrDefaultAsync(predicate);
        return entity;
    }

    public async Task<IEnumerable<TEntity>?> FindAllAsync(Expression<Func<TEntity, bool>> predicate)
    {
        IEnumerable<TEntity> entity = await _collection.Where(predicate).ToListAsync();
        return entity;
    }


    public async Task<TEntity?> FindAsync(int id)
    {
        var entity = await _collection.FindAsync(id);
        return entity;
    }
    public async Task<List<TEntity>> GetListAsync()
    {
        var list_entity = await _collection.ToListAsync();
        return list_entity;
    }
    public async Task UpdateAsync(TEntity entity)
    {
        // tim id = entity.id
        _collection.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<IDbContextTransaction> CreateTransaction()
    {
        var transaction = await _context.Database.BeginTransactionAsync();
        return transaction;
    }
}
