using SimpleLibrary.Application.Contracts.Services;

namespace SimpleLibrary.Application;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.UtcNow;
}
