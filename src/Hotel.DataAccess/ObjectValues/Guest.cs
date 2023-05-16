using Newtonsoft.Json;

namespace Hotel.DataAccess.ObjectValues;

public class Guest
{
    public string? Name { get; set; }
    public string? TelephoneNumber { get; set; }
    public string? Address { get; set; }
    public string? Type { get; set; }
    public string? PersonIdentification { get; set; }

    [JsonConstructor]
    public Guest(string? name, string? telephoneNumber, string? address, string? type, string? personIdentification)
    {
        Name = name;
        TelephoneNumber = telephoneNumber;
        Address = address;
        Type = type;
        PersonIdentification = personIdentification;
    }
}
