using System.Net;

namespace Hotel.Shared.Exceptions;

public class DomainBadRequestException : DomainException
{
    public DomainBadRequestException(string code, string message) : base(code, message)
    {
        this.HttpStatusCode = HttpStatusCode.BadRequest;
    }
}
