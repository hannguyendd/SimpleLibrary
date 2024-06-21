using System.Net;

namespace SimpleLibrary.Domain.Shared.Exceptions;

public class UnauthorizedException(string message) : HttpErrorException(HttpStatusCode.Unauthorized, message) { }