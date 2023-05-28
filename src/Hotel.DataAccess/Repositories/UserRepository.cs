using Hotel.DataAccess.Entities;
using System.Linq.Expressions;

namespace Hotel.DataAccess.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly IGenericRepository<User> _genericRepository;

    public UserRepository(
        IGenericRepository<User> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<User?> FindAsync(Expression<Func<User, bool>> predicate) 
        => await _genericRepository.FindAsync(predicate);

    // some delegate method
}
