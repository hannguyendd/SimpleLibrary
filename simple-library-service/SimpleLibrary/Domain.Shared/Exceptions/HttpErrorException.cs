using System.Net;

namespace SimpleLibrary.Domain.Shared.Exceptions;

public class HttpErrorException(HttpStatusCode status, string message) : Exception(message)
{
    public readonly HttpStatusCode Status = status;
}