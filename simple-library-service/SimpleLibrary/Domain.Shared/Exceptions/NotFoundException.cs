using System.Net;

namespace SimpleLibrary.Domain.Shared.Exceptions;

public class NotFoundException(string message) : HttpErrorException(HttpStatusCode.NotFound, message);