using System.Net;
using System.Text.Json;

namespace SimpleLibrary.Domain.Shared.Exceptions;

public class HttpErrorResponse
{
    public DateTime Timestamp { get; init; }
    public HttpStatusCode Status { get; init; }
    public string? Endpoint { get; init; }
    public string? Error { get; init; }
    public string? Message { get; init; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}