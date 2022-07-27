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
using Rossgram.Domain.Entities.Posts;
using Rossgram.Domain.Enumerations;
using Rossgram.Domain.Errors.Access;
using Rossgram.Domain.Views;

namespace Rossgram.Application.Histories.Queries.GetAccountHistories;

public class GetAccountHistoriesQueryHandler
    : IRequestHandler<GetAccountHistoriesQuery, GetAccountHistoriesResponseDTO>
{
    private readonly IHistoriesConfig _config;
    private readonly ITimeService _time;
    private readonly ObjectsStorageService _objectsStorage;
    private readonly IAppDbContext _context;

    public GetAccountHistoriesQueryHandler(
        IHistoriesConfig config,
        ITimeService time,
        ObjectsStorageService objectsStorage,
        IAppDbContext context)
    {
        _config = config;
        _time = time;
        _objectsStorage = objectsStorage;
        _context = context;
    }

    public async Task<GetAccountHistoriesResponseDTO> Handle(
        GetAccountHistoriesQuery request,
        CancellationToken cancellationToken)
    {
        // Base query
        IQueryable<History> query = _context.Histories;

        // Filter by follower
        query = query
            .Where(x => x.OwnerId == request.AccountId);

        // Include owner and histories
        query = query
            .Include(x => x.Owner)
            .ThenInclude(x => x.Avatar)
            .Include(x => x.UploadedFile);

        // Filter by time
        query = query
            .Where(x => x.CreatedAt > _time.Now.Add(-_config.TimeToArchive));

        // Pagination
        query = query
            .OrderByDescending(x => x.CreatedAt)
            .Take(_config.MaxCountPerAccount);

        // Query data
        List<History> histories = await query.ToListAsync(cancellationToken);

        return new GetAccountHistoriesResponseDTO(
            histories: histories.Select(y => new HistoryBaseDTO(
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
        );
    }
}

public class GetAccountHistoriesResponseDTO
{
    public List<HistoryBaseDTO> Histories { get; }

    public GetAccountHistoriesResponseDTO(
        List<HistoryBaseDTO> histories)
    {
        Histories = histories;
    }
}