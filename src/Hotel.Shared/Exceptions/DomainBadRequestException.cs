using System.Net;

namespace Hotel.Shared.Exceptions;

public class DomainBadRequestException : DomainException
{
    public HttpStatusCode HttpStatusCode { get; }
    public DomainBadRequestException(string code, string message) : base(code, message)
    {
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
}
