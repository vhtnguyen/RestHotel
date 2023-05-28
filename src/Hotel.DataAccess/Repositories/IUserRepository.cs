using Hotel.DataAccess.Entities;
using System.Linq.Expressions;

namespace Hotel.DataAccess.Repositories;

public interface IUserRepository
{
    Task<User?> FindAsync(Expression<Func<User, bool>> predicate);
}
