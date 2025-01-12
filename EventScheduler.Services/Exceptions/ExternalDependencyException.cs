using System.Net;

namespace EventScheduler.Services.Exceptions;

public class ExternalDependencyException:Exception
{
    public HttpStatusCode StatusCode { get; }
    
    public object? Payload { get; }
    
    public ExternalDependencyException(HttpStatusCode httpStatusCode, object? payload = null)
    {
        StatusCode = httpStatusCode;
        Payload = payload;
    }
}