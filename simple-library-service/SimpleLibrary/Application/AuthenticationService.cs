using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimpleLibrary.Application.Contracts.Dtos.Authentication;
using SimpleLibrary.Application.Contracts.Services;
using SimpleLibrary.Domain.Shared.Configurations;

namespace SimpleLibrary.Application;

public class AuthenticationService(IOptions<JwtSetting> jwtSetting)
{
    private readonly IOptions<JwtSetting> _jwtSetting = jwtSetting;


    public string GenerateJwtToken(ICollection<Claim> claims, int expireTimeInSecond, string secret)
    {
        var jwtToken = new JwtSecurityToken(
               claims: claims,
               notBefore: DateTime.UtcNow,
               expires: DateTime.UtcNow.AddSeconds(expireTimeInSecond),
               signingCredentials: new SigningCredentials(
                   new SymmetricSecurityKey(
                      Encoding.UTF8.GetBytes(secret)
                       ),
                   SecurityAlgorithms.HmacSha256Signature)
               );

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }

    // public Task<TokenDto> LoginAsync(LoginRequest request)
    // {

    // }
}