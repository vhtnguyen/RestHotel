using System.Net;

namespace Hotel.Shared.Exceptions;

public class DomainNotFoundException : DomainException
{
    public HttpStatusCode StatusCode { get; }
    public DomainNotFoundException(string code, string message) : base(code, message)
    {
        StatusCode = HttpStatusCode.NotFound;
    }

}
