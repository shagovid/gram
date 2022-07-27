using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Rossgram.Application.Common.Interfaces;
using Rossgram.Application.Common.Interfaces.Configs;
using Rossgram.Domain.Enumerations;
using Rossgram.Domain.ValueObjects;

namespace Rossgram.Application.Common.Services;

public class AuthService
{
    public const string ID_CLAIM_NAME = ClaimTypes.NameIdentifier;
    public const string LOGIN_CLAIM_NAME = ClaimTypes.Name;
    public const string ROLE_CLAIM_NAME = ClaimTypes.Role;

    private readonly IAuthConfig _config;
    private readonly ITimeService _time;

    public AuthService(
        IAuthConfig config,
        ITimeService time)
    {
        _config = config;
        _time = time;
    }

    public string CreateTokenWith(long id, string nickname, Role role)
    {
        DateTimeOffset now = _time.Now;

        JwtSecurityToken token = new (
            issuer: _config.JwtIssuer,
            audience: _config.JwtAudience,
            claims: new Claim[]
            {
                new (ID_CLAIM_NAME, id.ToString()),
                new (LOGIN_CLAIM_NAME, nickname),
                new (ROLE_CLAIM_NAME, role.Id.ToString()),
            },
            notBefore: now.UtcDateTime,
            expires: now.Add(_config.JwtLifetime).UtcDateTime,
            signingCredentials: new SigningCredentials(
                key: _config.JwtSigningKey,
                algorithm: SecurityAlgorithms.HmacSha256));

        string encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return encodedToken;
    }
}