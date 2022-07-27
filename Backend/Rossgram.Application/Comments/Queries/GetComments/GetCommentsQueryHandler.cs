using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rossgram.Application.Common.BaseDTOs;
using Rossgram.Application.Common.Interfaces;
using Rossgram.Application.Common.Services;
using Rossgram.Domain.Views;

namespace Rossgram.Application.Comments.Queries.GetComments;

public class GetCommentsQueryHandler
    : IRequestHandler<GetCommentsQuery, GetCommentsResponseDTO>
{
    private readonly ICurrentAuth _auth;
    private readonly ObjectsStorageService _objectsStorage;
    private readonly IAppDbContext _context;

    public GetCommentsQueryHandler(
        ICurrentAuth auth,
        ObjectsStorageService objectsStorage,
        IAppDbContext context)
    {
        _auth = auth;
        _objectsStorage = objectsStorage;
        _context = context;
    }

    public async Task<GetCommentsResponseDTO> Handle(
        GetCommentsQuery request,
        CancellationToken cancellationToken)
    {
        // Base query
        IQueryable<PostCommentView> query = _context.PostsCommentsView;

        // Filter by post
        query = query
            .Where(x => x.PostId == request.PostId);

        // Include owner
        query = query
            .Include(x => x.Owner)
            .ThenInclude(x => x.Avatar);

        // Include account like
        if (_auth.IsAuthorized)
        {
            query = query
                .Include(x => x.Likes.Where(y => y.OwnerId == _auth.Id));
        }

        // Pagination
        int offset = Math.Max(request.Offset ?? 0, 0);
        int limit = Math.Clamp(request.Limit ?? 20, 0, 100);
        query = query
            .OrderByDescending(x => x.CreatedAt)
            .Skip(offset).Take(limit);

        // Query data
        List<PostCommentView> comments = await query.ToListAsync(cancellationToken);

        return new GetCommentsResponseDTO(
            comments: comments.Select(x => new PostCommentBaseDTO(
                    id: x.Id,
                    owner: new AccountShortBaseDTO(
                        id: x.Owner.Id,
                        nickname: x.Owner.Nickname,
                        isVerified: x.Owner.IsVerified,
                        avatarLink: x.Owner.Avatar is not null
                            ? _objectsStorage.GetLinkFor(x.Owner.Avatar.ObjectsStorageKey)
                            : null),
                    createdAt: x.CreatedAt,
                    text: x.Text.ToString(),
                    likesCount: x.LikesCount,
                    isLiked: x.Likes.Any()
                )
            ).ToList()
        );
    }
}

public class GetCommentsResponseDTO
{
    public List<PostCommentBaseDTO> Comments { get; }

    public GetCommentsResponseDTO(
        List<PostCommentBaseDTO> comments)
    {
        Comments = comments;
    }
}