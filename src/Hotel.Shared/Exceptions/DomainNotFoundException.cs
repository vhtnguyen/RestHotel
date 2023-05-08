using System.Net;

namespace Hotel.Shared.Exceptions;

public class DomainNotFoundException : DomainException
{
    public string Code { get; }
    public HttpStatusCode StatusCode { get; }
    public DomainNotFoundException(string code, string message) : base(message)
    {
        Code = code;
        StatusCode = HttpStatusCode.NotFound;
    }

}
