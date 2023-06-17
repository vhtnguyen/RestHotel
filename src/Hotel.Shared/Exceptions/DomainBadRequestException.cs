using System.Net;

namespace Hotel.Shared.Exceptions;

public class DomainBadRequestException : DomainException
{
    public DomainBadRequestException(string message, string code) : base(code, message)
    {
        this.HttpStatusCode = HttpStatusCode.BadRequest;
    }
}
