using System.Net;

namespace SimpleLibrary.Domain.Shared.Exceptions;

public class BadRequestException(string message) : HttpErrorException(HttpStatusCode.BadRequest, message);