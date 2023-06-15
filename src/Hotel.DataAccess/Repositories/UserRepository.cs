using Hotel.DataAccess.Context;
using Hotel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using org.apache.zookeeper.data;
using Hotel.DataAccess.Repositories;
using System.Linq;

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
           .Include(u => u.Role)
           .ToListAsync();
        return result;
    }
    public async Task<User?> FindAsync(int id)
    {
        var user = await _context.Users.Where(u=>u.Id==id)
           .Include(u => u.Role).FirstOrDefaultAsync();

        return user;
    }
     

    //// some delegate method

    public Task<IEnumerable<User>> BrowserAsync()
    {
        throw new NotImplementedException();
    }
  
    public async Task<User> CreateAsync(User entity)
        => await _genericRepository.CreateAsync(entity);
    public async Task UpdateAsync(User entity)
    => await _genericRepository.UpdateAsync(entity);
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
            throw new NotImplementedException();

        }
        
    }

    public async Task<IEnumerable<User>?> FindAllAsync(Expression<Func<User, bool>> predicate)
    {
        return await _context.Users.Include(u=>u.Role).Where(predicate).ToListAsync();
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

   