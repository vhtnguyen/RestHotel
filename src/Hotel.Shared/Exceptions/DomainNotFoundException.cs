using System.Net;

namespace Hotel.Shared.Exceptions;

public class DomainNotFoundException : DomainException
{
    public DomainNotFoundException(string code, string message) : base(code, message)
    {
        this.HttpStatusCode = HttpStatusCode.NotFound;
    }

}
