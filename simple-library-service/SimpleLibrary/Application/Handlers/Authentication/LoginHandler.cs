using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimpleLibrary.Application.Contracts.Commands.Authentication;
using SimpleLibrary.Application.Contracts.Dtos.Authentication;
using SimpleLibrary.Domain.Entities;
using SimpleLibrary.Domain.Repositories;
using SimpleLibrary.Domain.Shared.Configurations;
using SimpleLibrary.Domain.Shared.Exceptions;
using SimpleLibrary.Domain.Shared.Utilities;

namespace SimpleLibrary.Application.Handlers.Authentication;

public class LoginHandler(IAccountRepository accountRepository, IOptions<JwtSetting> jwtSetting) : IRequestHandler<LoginCommand, TokenDto>
{

    public async Task<TokenDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var account = await accountRepository.GetByUsernameAsync(request.Credentials.Username) ?? throw new UnauthorizedException("Invalid credentials");

        if (!HashPasswordUtility.IsValidPassword(request.Credentials.Password, account.HashPassword, account.Salt))
        {
            throw new UnauthorizedException("Invalid credentials");
        }

        var expiredAt = DateTime.UtcNow.AddSeconds(3600 * 24);

        return new TokenDto(
            GenerateJwtToken(GetAccountClaims(account), expiredAt, jwtSetting.Value.Secret),
            expiredAt);
    }

    private static ICollection<Claim> GetAccountClaims(Account account)
    {
        return [
            new(ClaimTypes.NameIdentifier, account.Id.ToString()),
            new(ClaimTypes.Name, account.Username),
        ];
    }

    public string GenerateJwtToken(ICollection<Claim> claims, DateTime expiredAt, string secret)
    {
        var jwtToken = new JwtSecurityToken(
               claims: claims,
               notBefore: DateTime.UtcNow,
               expires: expiredAt,
               signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                SecurityAlgorithms.HmacSha256Signature)
            );

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}
