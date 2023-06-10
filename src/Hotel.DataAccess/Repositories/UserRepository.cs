using Hotel.DataAccess.Context;
using Hotel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using org.apache.zookeeper.data;
using Hotel.DataAccess.Repositories;

namespace Hotel.DataAccess.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly IGenericRepository<User> _genericRepository;
    private readonly AppDbContext _context;

    public UserRepository(
        IGenericRepository<User> genericRepository, AppDbContext context)
    {
        _genericRepository = genericRepository;
        _context = context;
    }

    public async Task<User?> FindAsync(Expression<Func<User, bool>> predicate)
        => await _genericRepository.FindAsync(predicate);

    public async Task<IEnumerable<User>?> GetListAsync()
    {
        var result = await _context.Users
           .Include(u => u.Roles)
           .ToListAsync();
        return result;
    }
     

    //// some delegate method

    public Task<IEnumerable<User>> BrowserAsync()
    {
        throw new NotImplementedException();
    }
    public Task<User?> FindAsync(Guid id)
    {
        throw new NotImplementedException();
    }
    public async Task<User> CreateAsync(User entity)
        => await _genericRepository.CreateAsync(entity);
    public Task UpdateAsync(User entity)
    {
        throw new NotImplementedException();
    }
    public async Task DeleteByIDAsync(int userId)
    { 
        var user_to_remove= await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if(user_to_remove != null)
        {
            _context.Users.Remove(user_to_remove);
            await _context.SaveChangesAsync();
        }
        else
        {

        }
        
    }
    public Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
    public Task<IDbContextTransaction> CreateTransaction()
    {
        throw new NotImplementedException();
    }
}

   