using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rossgram.Application.Common.BaseDTOs;
using Rossgram.Application.Common.Interfaces;
using Rossgram.Application.Common.Services;
using Rossgram.Domain;
using Rossgram.Domain.Entities;
using Rossgram.Domain.Enumerations;
using Rossgram.Domain.Errors.Access;
using Rossgram.Domain.Errors.ValueObjects.Nickname;
using Rossgram.Domain.ValueObjects;

namespace Rossgram.Application.Auth.Commands.SignUp;

public class SignUpCommandHandler
    : IRequestHandler<SignUpCommand, SignUpResponseDTO>
{
    private readonly ICurrentAuth _auth;
    private readonly NicknameController _nicknameController;
    private readonly PasswordController _passwordController;
    private readonly EmailController _emailController;
    private readonly AuthService _authService;
    private readonly IAppDbContext _context;

    public SignUpCommandHandler(
        ICurrentAuth auth,
        NicknameController nicknameController,
        PasswordController passwordController,
        EmailController emailController,
        AuthService authService,
        IAppDbContext context)
    {
        _auth = auth;
        _nicknameController = nicknameController;
        _passwordController = passwordController;
        _emailController = emailController;
        _authService = authService;
        _context = context;
    }

    public async Task<SignUpResponseDTO> Handle(
        SignUpCommand request,
        CancellationToken cancellationToken)
    {
        // Check creating role
        if (request.Account.Role != Role.User)
        {
            if (!_auth.IsAuthorized) throw new UnauthorizedAccessError();
                
            if (request.Account.Role == Role.Admin && _auth.Role.Permissions.NotContains(Permission.CanCreateAdmin))
                throw new ForbiddenAccessError();
            if (request.Account.Role == Role.Moderator && _auth.Role.Permissions.NotContains(Permission.CanCreateModer))
                throw new ForbiddenAccessError();
        }
        
        // Check verified
        if (request.Account.IsVerified)
        {
            if (!_auth.IsAuthorized) throw new UnauthorizedAccessError();
                
            if (_auth.Role.Permissions.NotContains(Permission.CanChangeIsVerified))
                throw new ForbiddenAccessError();
        }
        
        // Check is nickname reserved
        ReservedNickname? reservedNickname = await _context.ReservedNicknames
            .Where(x => x.Nickname == request.Account.Nickname)
            .FirstOrDefaultAsync(cancellationToken);
        if (reservedNickname is not null)
        {
            if (string.IsNullOrEmpty(request.NicknameKey)) 
                throw new NicknameKeyRequiredError(request.Account.Nickname);

            if (request.NicknameKey != reservedNickname.Key) 
                throw new InvalidNicknameKeyError(request.Account.Nickname);
        }
        
        // Create account
        Account account = new(
            id: default,
            role: request.Account.Role,
            nickname: await _nicknameController.Validate(request.Account.Nickname),
            password: _passwordController.Create(request.Account.Password), 
            email: await _emailController.Validate(request.Account.Email),
            name: request.Account.Name,
            bio: request.Account.Bio,
            isVerified: request.Account.IsVerified,
            avatarId: null,
            avatar: default,
            uploadedFiles: default!,
            histories: default!,
            posts: default!, 
            postsLikes: default!, 
            postsComments: default!, 
            postCommentsLikes: default!, 
            followings: default!, 
            followers: default!, 
            privateConversationsAsOlder: default!,
            privateConversationsAsNewer: default!,
            groupsConversationsMember: default!,
            sentMessages: default!, 
            messagesLikes: default!);
        await _context.Accounts.AddAsync(account, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        // Create token
        string token = _authService.CreateTokenWith(
            id: account.Id,
            nickname: account.Nickname,
            role: account.Role);
            
        // Return result
        return new SignUpResponseDTO(
            jwtToken: token,
            account: new AccountBaseDTO(
                id: account.Id,
                nickname: account.Nickname.ToString(),
                isVerified: account.IsVerified,
                avatarLink: null,
                name: account.Name,
                bio: account.Bio,
                followerCount: 0,
                followingCount: 0,
                postsCount: 0
            )
        );
    }
}

public class SignUpResponseDTO
{
    public string JwtToken { get; }
    public AccountBaseDTO Account { get; }

    public SignUpResponseDTO(
        string jwtToken, 
        AccountBaseDTO account)
    {
        JwtToken = jwtToken;
        Account = account;
    }
}