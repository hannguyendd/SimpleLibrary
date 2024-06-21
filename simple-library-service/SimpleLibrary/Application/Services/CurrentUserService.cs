using System.Security.Claims;
using SimpleLibrary.Application.Contracts.Services;

namespace SimpleLibrary.Application.Services;

public class CurrentUserService : ICurrentUserService
{
    public Guid UserId { get; private set; }

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        var userId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        UserId = string.IsNullOrWhiteSpace(userId) ? Guid.Empty : new Guid(userId);
    }
}