﻿using Hotel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Hotel.DataAccess.Repositories.IRepositories;

public interface IUserRepository
{
    Task<User?> FindAsync(Expression<Func<User, bool>> predicate);
    Task<User?> FindAsync(int id);
    Task<IEnumerable<User>?> GetListAsync();

    Task<IEnumerable<User>> BrowserAsync();
    Task<User> CreateAsync(User entity);
    Task UpdateAsync(User entity);
    Task DeleteByIDAsync(int id);
    Task SaveChangesAsync();
    Task<IEnumerable<User>?> FindAllAsync(Expression<Func<User, bool>> predicate);
    Task<IDbContextTransaction> CreateTransaction();
}
