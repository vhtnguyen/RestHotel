using System.Net;

namespace Hotel.Shared.Exceptions;

public class DomainBadRequestException : DomainException
{
    public string Code { get; }
    public HttpStatusCode HttpStatusCode { get; }
    public DomainBadRequestException(string code, string message) : base(message)
    {
        Code = code;
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
}
