using Newtonsoft.Json;

namespace Hotel.DataAccess.Entities;

public class Role
{
    public int Id { get; set; }
    public string? NameType { get; set; }

    // navigation property
    public virtual ICollection<User> Users { get; set; } = new List<User>();

    [JsonConstructor]
    public Role(int id, string? nameType)
    {
        Id = id;
        NameType = nameType;
    }
}
