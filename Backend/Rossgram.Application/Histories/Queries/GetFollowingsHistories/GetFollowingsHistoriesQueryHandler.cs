using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rossgram.Application.Common.BaseDTOs;
using Rossgram.Application.Common.Interfaces;
using Rossgram.Application.Common.Interfaces.Configs;
using Rossgram.Application.Common.Services;
using Rossgram.Domain.Entities;

namespace Rossgram.Application.Histories.Queries.GetFollowingsHistories;

public class GetFollowingsHistoriesQueryHandler
    : IRequestHandler<GetFollowingsHistoriesQuery, GetFollowingsHistoriesResponseDTO>
{
    private readonly IHistoriesConfig _config;
    private readonly ICurrentAuth _auth;
    private readonly ITimeService _time;
    private readonly ObjectsStorageService _objectsStorage;
    private readonly IAppDbContext _context;

    public GetFollowingsHistoriesQueryHandler(
        IHistoriesConfig config,
        ICurrentAuth auth,
        ITimeService time,
        ObjectsStorageService objectsStorage,
        IAppDbContext context)
    {
        _config = config;
        _auth = auth;
        _time = time;
        _objectsStorage = objectsStorage;
        _context = context;
    }

    public async Task<GetFollowingsHistoriesResponseDTO> Handle(
        GetFollowingsHistoriesQuery request,
        CancellationToken cancellationToken)
    {
        // Base query
        IQueryable<Following> query = _context.Followings;

        // Filter by follower
        query = query
            .Where(x => x.FollowerId == _auth.Id);

        // Include owner and histories
        query = query
            .Include(x => x.Account)
            .ThenInclude(x => x.Avatar)
            .Include(x => x.Account.Histories
                .Where(y => y.CreatedAt > _time.Now.Add(-_config.TimeToArchive))
            )
            .ThenInclude(x => x.UploadedFile);

        // Pagination
        int offset = Math.Max(request.Offset ?? 0, 0);
        int limit = Math.Clamp(request.Limit ?? 20, 0, 100);
        query = query
            .OrderByDescending(x => x.Account.Histories
                .Select(y => y.CreatedAt).Max()
            )
            .Skip(offset).Take(limit);

        // Query data
        List<Following> followings = await query.ToListAsync(cancellationToken);

        return new GetFollowingsHistoriesResponseDTO(
            followings: followings.Select(x => new GetFollowingsHistoriesResponseDTO.FollowingDTO(
                    id: x.Account.Id,
                    nickname: x.Account.Nickname,
                    isVerified: x.Account.IsVerified,
                    avatarLink: x.Account.Avatar is not null
                        ? _objectsStorage.GetLinkFor(x.Account.Avatar.ObjectsStorageKey)
                        : null,
                    histories: x.Account.Histories.Select(y => new HistoryBaseDTO(
                            id: y.Id,
                            createdAt: y.CreatedAt,
                            file: new UploadedFileBaseDTO(
                                id: y.UploadedFile.Id,
                                type: y.UploadedFile.Type,
                                fullName: y.UploadedFile.FullName,
                                link: _objectsStorage.GetLinkFor(y.UploadedFile.ObjectsStorageKey)
                            )
                        )
                    ).ToList()
                )
            ).ToList()
        );
    }
}

public class GetFollowingsHistoriesResponseDTO
{
    public List<FollowingDTO> Followings { get; }

    public GetFollowingsHistoriesResponseDTO(
        List<FollowingDTO> followings)
    {
        Followings = followings;
    }
    
    public class FollowingDTO : AccountShortBaseDTO
    {
        public List<HistoryBaseDTO> Histories { get; }

        public FollowingDTO(
            long id, 
            string nickname, 
            bool isVerified, 
            string? avatarLink, 
            List<HistoryBaseDTO> histories) 
            : base(id, nickname, isVerified, avatarLink)
        {
            Histories = histories;
        }
    }
}