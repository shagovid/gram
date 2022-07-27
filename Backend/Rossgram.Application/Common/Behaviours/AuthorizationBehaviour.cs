using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Rossgram.Application.Common.Interfaces;
using Rossgram.Domain;
using Rossgram.Domain.Enumerations;
using Rossgram.Domain.Errors.Access;

namespace Rossgram.Application.Common.Behaviours;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
{
    private readonly ICurrentAuth _auth;

    public AuthorizationBehaviour(
        ICurrentAuth auth)
    {
        _auth = auth;
    }
        
    public async Task<TResponse> Handle(
        TRequest request, 
        CancellationToken cancellationToken, 
        RequestHandlerDelegate<TResponse> next)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        Type requestType = request.GetType();

        List<AuthorizeAttribute> authorizeAttributes = requestType
            .GetCustomAttributes<AuthorizeAttribute>()
            .ToList();
        
        // Наличие аттрибута Authorize требует авторизацию
        if (authorizeAttributes.Any() && !_auth.IsAuthorized)
            throw new UnauthorizedAccessError();
            
        // Продолжаем пайплайн
        return await next();
    }
}

/// <summary>
/// Указывает, что данный запрос (IRequest) требует наличие авторизации
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class AuthorizeAttribute : Attribute
{
    
}