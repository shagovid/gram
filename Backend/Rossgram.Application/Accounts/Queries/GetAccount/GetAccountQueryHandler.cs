using System;
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

namespace Rossgram.Application.Accounts.Queries.GetAccount;

public class GetAccountQueryHandler
    : IRequestHandler<GetAccountQuery, GetAccountResponseDTO>
{
    private readonly ObjectsStorageService _objectsStorage;
    private readonly IAppDbContext _context;

    public GetAccountQueryHandler(
        ObjectsStorageService objectsStorage,
        IAppDbContext context)
    {
        _objectsStorage = objectsStorage;
        _context = context;
    }

    public async Task<GetAccountResponseDTO> Handle(
        GetAccountQuery request,
        CancellationToken cancellationToken)
    {
        AccountView? account = await _context.AccountsView
            .Include(x => x.Avatar)
            .FirstOrDefaultAsync(x => x.Id == request.AccountId, cancellationToken);
        if (account is null) throw new NotImplementedException();

        return new GetAccountResponseDTO(
            account: new AccountBaseDTO(
                id: account.Id,
                nickname: account.Nickname,
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

public class GetAccountResponseDTO
{
    public AccountBaseDTO Account { get; }

    public GetAccountResponseDTO(
        AccountBaseDTO account)
    {
        Account = account;
    }
}