using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rossgram.Application.Accounts.Queries.GetAccount;
using Rossgram.Application.Common.BaseDTOs;
using Rossgram.Application.Common.Interfaces;
using Rossgram.Application.Common.Services;
using Rossgram.Domain.Entities;
using Rossgram.Domain.Enumerations;
using Rossgram.Domain.Errors.Auth;
using Rossgram.Domain.Views;

namespace Rossgram.Application.Accounts.Queries.GetAccountFollowers;

public class GetAccountFollowersQueryHandler
    : IRequestHandler<GetAccountFollowersQuery, GetAccountFollowersResponseDTO>
{
    private readonly ObjectsStorageService _objectsStorage;
    private readonly IAppDbContext _context;

    public GetAccountFollowersQueryHandler(
        ObjectsStorageService objectsStorage,
        IAppDbContext context)
    {
        _objectsStorage = objectsStorage;
        _context = context;
    }

    public async Task<GetAccountFollowersResponseDTO> Handle(
        GetAccountFollowersQuery request,
        CancellationToken cancellationToken)
    {
        List<Account> followers = await _context.Followings
            .Where(x => x.AccountId == request.AccountId)
            .Include(x => x.Account)
            .ThenInclude(x => x.Avatar)
            .Select(x => x.Account)
            .ToListAsync(cancellationToken);

        return new GetAccountFollowersResponseDTO(
            followers: followers.Select(x => new AccountShortBaseDTO(
                id: x.Id,
                nickname: x.Nickname,
                isVerified: x.IsVerified,
                avatarLink: x.Avatar is not null
                    ? _objectsStorage.GetLinkFor(x.Avatar.ObjectsStorageKey)
                    : null
            )).ToList()
        );
    }
}

public class GetAccountFollowersResponseDTO
{
    public List<AccountShortBaseDTO> Followers { get; }

    public GetAccountFollowersResponseDTO(
        List<AccountShortBaseDTO> followers)
    {
        Followers = followers;
    }
}