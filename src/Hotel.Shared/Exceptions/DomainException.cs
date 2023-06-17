using System.Net;

namespace Hotel.Shared.Exceptions;

public class DomainException : Exception
{
    public string Code { get; }
    virtual public HttpStatusCode HttpStatusCode { get; protected set; }
    public DomainException(string message, string code) : base(message)
    {
        Code = code;
    }

}
