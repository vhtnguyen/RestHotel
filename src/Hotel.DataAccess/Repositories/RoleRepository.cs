using Hotel.DataAccess.Entities;
using Hotel.DataAccess.Repositories.IRepositories;
using System.Linq.Expressions;

namespace Hotel.DataAccess.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly IGenericRepository<Role> _genericRepository;
    public RoleRepository(IGenericRepository<Role> repository)
    {
        _genericRepository = repository;
    }
    public async Task<Role?> FindAsync(Expression<Func<Role, bool>> predicate)
    => await _genericRepository.FindAsync(predicate);

    public async Task<List<Role>?> FindAllAsync(Expression<Func<Role, bool>> predicate)
    {
        var list_role = new List<Role>();
        List<Role>? all_roles = await _genericRepository.GetListAsync();
        if (all_roles != null)
        {
            list_role = all_roles.Where(predicate.Compile()).ToList();
        }
        return list_role;
    }

    public Task Create(Role role)
    {
        return _genericRepository.CreateAsync(role);
    }
}
