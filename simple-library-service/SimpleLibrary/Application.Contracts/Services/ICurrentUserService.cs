namespace SimpleLibrary.Application.Contracts.Services;

public interface ICurrentUserService
{
    Guid UserId { get; }
}