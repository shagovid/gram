using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rossgram.Application.Common.BaseDTOs;
using Rossgram.Application.Common.Interfaces;
using Rossgram.Application.Common.Services;
using Rossgram.Domain.Entities;
using Rossgram.Domain.Enumerations;
using Rossgram.Domain.Errors.Auth;
using Rossgram.Domain.Views;

namespace Rossgram.Application.Auth.Commands.SignIn;

public class SignInQueryHandler
    : IRequestHandler<SignInCommand, SignInResponseDTO>
{
    private readonly ObjectsStorageService _objectsStorage;
    private readonly PasswordController _passwordController;
    private readonly AuthService _authService;
    private readonly IAppDbContext _context;

    public SignInQueryHandler(
        ObjectsStorageService objectsStorage,
        PasswordController passwordController,
        AuthService authService,
        IAppDbContext context)
    {
        _objectsStorage = objectsStorage;
        _passwordController = passwordController;
        _authService = authService;
        _context = context;
    }

    public async Task<SignInResponseDTO> Handle(
        SignInCommand request,
        CancellationToken cancellationToken)
    {
        AccountView? account = await _context.AccountsView
            .Include(x => x.Avatar)
            .FirstOrDefaultAsync(
                predicate: x => x.Nickname == request.Login || x.Email == request.Login, 
                cancellationToken: cancellationToken);
        if (account is null) throw new InvalidLoginOrPasswordError();
            
        bool isPasswordValid = _passwordController.Compare(
            encryptedPassword: account.Password,
            value: request.Password
        );
        if (!isPasswordValid) throw new InvalidLoginOrPasswordError();
            
        string token = _authService.CreateTokenWith(
            id: account.Id, 
            nickname: account.Nickname,
            role: account.Role);

        return new SignInResponseDTO(
            jwtToken: token,
            account: new AccountBaseDTO(
                id: account.Id,
                nickname: account.Nickname.ToString(),
                isVerified: account.IsVerified,
                avatarLink: account.Avatar is not null
                    ? _objectsStorage.GetLinkFor(account.Avatar.ObjectsStorageKey)
                    : null,
                name: account.Name,
                bio: account.Bio,
                followerCount: account.FollowerCount,
                followingCount: account.FollowingCount,
                postsCount: account.PostsCount
            )
        );
    }
}

public class SignInResponseDTO
{
    public string JwtToken { get; }
    public AccountBaseDTO Account { get; }

    public SignInResponseDTO(
        string jwtToken, 
        AccountBaseDTO account)
    {
        JwtToken = jwtToken;
        Account = account;
    }
}