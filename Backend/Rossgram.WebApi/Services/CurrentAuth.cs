using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Rossgram.Application.Common.Interfaces;
using Rossgram.Application.Common.Services;
using Rossgram.Domain.Enumerations;
using Rossgram.Domain.Errors.Access;

namespace Rossgram.WebApi.Services;

public class CurrentAuth : ICurrentAuth
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public bool IsAuthorized => _httpContextAccessor.HttpContext?.User
        ?.FindFirst(AuthService.ID_CLAIM_NAME) != null;

    public long Id => GetValue(AuthService.ID_CLAIM_NAME, x => 
        long.TryParse(x, out long id) ? id : throw new UnauthorizedAccessError()
    );
    public string Username => GetValue(AuthService.LOGIN_CLAIM_NAME, x => x);
    public Role Role => GetValue(AuthService.ROLE_CLAIM_NAME, x => int.TryParse(x, out int roleId)
        ? Enumeration.TryGetById(roleId, out Role? role) ? role! : throw new UnauthorizedAccessError()
        : throw new UnauthorizedAccessError()
    );
        
    public CurrentAuth(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private T GetValue<T>(string claimName, Func<string, T> parser)
    {
        Claim? claim = _httpContextAccessor.HttpContext?.User?.FindFirst(claimName);
        if (claim is null) throw new UnauthorizedAccessError();
        return parser(claim.Value);
    }
}