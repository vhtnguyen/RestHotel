using System.Net;

namespace Hotel.Shared.Exceptions;

public class DomainNotFoundException : DomainException
{
    public DomainNotFoundException(string message, string code) : base(code, message)
    {
        this.HttpStatusCode = HttpStatusCode.NotFound;
    }

}
