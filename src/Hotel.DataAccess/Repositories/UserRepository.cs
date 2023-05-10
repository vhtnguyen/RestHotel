using Hotel.DataAccess.Entities;

namespace Hotel.DataAccess.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly IGenericRepository<User> _genericRepository;

    public UserRepository(
		IGenericRepository<User> genericRepository)
	{
        _genericRepository = genericRepository;
    }

    // some delegate method
}
