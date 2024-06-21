using SimpleLibrary.Application.Contracts.Dtos.Authentication;

namespace SimpleLibrary.Application.Contracts.Services;

public interface IAuthService
{
    Task<TokenDto> LoginAsync(LoginRequest request);
}