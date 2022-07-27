using System;
using Microsoft.IdentityModel.Tokens;

namespace Rossgram.Application.Common.Interfaces.Configs;

public interface IAuthConfig
{
    public string JwtIssuer { get; }
    public string JwtAudience { get; }
    public TimeSpan JwtLifetime { get; }
    public SecurityKey JwtSigningKey { get; }
}