using Hotel.Shared.Exceptions;
using Newtonsoft.Json;

namespace Hotel.DataAccess.Entities;

public class User
{
    public int Id { get; set; }
    public string? Account { get; set; }
    public string? Password { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? TelephoneNumber { get; set; }
    public string? Avatar { get; set; }

    // some reference field
    public ICollection<Role> Roles { get; set; } = new List<Role>();

    [JsonConstructor]
    public User(int id, string? account, string? password, string? fullName, string? email, string? telephoneNumber, string? avatar)
    {
        Id = id;
        Account = account;
        Password = password;
        FullName = fullName;
        Email = email;
        TelephoneNumber = telephoneNumber;
        Avatar = avatar;
    }
    public User() { }

    // some method
    public void AddRole(Role role)
    {
        // find and add role
        var isExistRole = Roles.Any(r => r.Id == role.Id);
        if(isExistRole)
        {
            // throw exception here
            throw new DomainBadRequestException(
                "role_has_existed",
                $"role '{role.NameType}' has existed on user {FullName}");
        }

        Roles.Add(role);
    }

    public void RemoveRole(Role role)
    {
        // find and add role
        var isExistRole = Roles.Any(r => r.Id == role.Id);
        if (!isExistRole)
        {
            // throw exception here
            throw new DomainBadRequestException(
                "role_dosen't_exist",
                $"role '{role.NameType}' doesn't exist on user {FullName}");
        }

        Roles.Remove(role);
    }
    
}
