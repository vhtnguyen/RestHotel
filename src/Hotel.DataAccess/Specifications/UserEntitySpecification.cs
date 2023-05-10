using Hotel.DataAccess.Entities;
using Hotel.Shared.Exceptions;

namespace Hotel.DataAccess.Specifications;

public class UserEntitySpecification : IEntitySpecification<User>
{
    private static UserEntitySpecification? _instance;
    public static UserEntitySpecification GetInstance()
    {
        if(_instance == null)
        {
            _instance = new UserEntitySpecification();
        }
        return _instance;
    }
    public IEnumerable<string> IsNotValidBecause(User entity)
    {
        if(entity.Id == Guid.Empty)
        {
            yield return "User Id cannot be empty.";
        }
    }

    public void ThrowErrorIfNotValid(User entity)
    {
        var result = IsNotValidBecause(entity).GetEnumerator();
        while(result.MoveNext())
        {
            // throw exception here
            throw new DomainBadRequestException("user_invalid_input", result.Current);
        }
    }
}
