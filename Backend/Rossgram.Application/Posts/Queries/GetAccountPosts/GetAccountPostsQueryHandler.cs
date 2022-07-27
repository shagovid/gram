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
using Rossgram.Domain.Entities.Posts;
using Rossgram.Domain.Enumerations;
using Rossgram.Domain.Errors.Access;
using Rossgram.Domain.Views;

namespace Rossgram.Application.Posts.Queries.GetAccountPosts;

public class GetAccountPostsQueryHandler
    : IRequestHandler<GetAccountPostsQuery, GetAccountPostsResponseDTO>
{
    private readonly ICurrentAuth _auth;
    private readonly ObjectsStorageService _objectsStorage;
    private readonly IAppDbContext _context;

    public GetAccountPostsQueryHandler(
        ICurrentAuth auth,
        ObjectsStorageService objectsStorage,
        IAppDbContext context)
    {
        _auth = auth;
        _objectsStorage = objectsStorage;
        _context = context;
    }

    public async Task<GetAccountPostsResponseDTO> Handle(
        GetAccountPostsQuery request,
        CancellationToken cancellationToken)
    {
        // Base query
        IQueryable<PostView> query = _context.PostsView;

        // Filter by owner
        query = query
            .Where(x => x.OwnerId == request.AccountId);

        // Include owner and media
        query = query
            .Include(x => x.Owner)
            .ThenInclude(x => x.Avatar)
            .Include(x => x.Attachments)
            .ThenInclude(x => (x as PostAttachmentFile)!.UploadedFile);

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
        List<PostView> posts = await query.ToListAsync(cancellationToken);

        return new GetAccountPostsResponseDTO(
            posts: posts.Select(x => new PostBaseDTO(
                        id: x.Id,
                        owner: new AccountShortBaseDTO(
                            id: x.Owner.Id,
                            nickname: x.Owner.Nickname.ToString(),
                            isVerified: x.Owner.IsVerified,
                            avatarLink: x.Owner.Avatar is not null
                                ? _objectsStorage.GetLinkFor(x.Owner.Avatar.ObjectsStorageKey)
                                : null
                        ),
                        createdAt: x.CreatedAt,
                        attachments: x.Attachments.Select(y => y switch
                            {
                                PostAttachmentFile attachmentFile => (AttachmentBaseDTO)
                                    new FileAttachmentBaseDTO(
                                        id: attachmentFile.Id,
                                        order: attachmentFile.Order,
                                        file: new UploadedFileBaseDTO(
                                            id: attachmentFile.UploadedFile.Id,
                                            type: attachmentFile.UploadedFile.Type,
                                            fullName: attachmentFile.UploadedFile.FullName,
                                            link: _objectsStorage.GetLinkFor(attachmentFile.UploadedFile
                                                .ObjectsStorageKey
                                            )
                                        )
                                    ),
                                _ => throw new NotImplementedException()
                            }
                        ).ToList(),
                        comment: x.Comment.ToString(),
                        likesCount: x.LikesCount,
                        isLiked: x.Likes.Any(),
                        commentsCount: x.CommentsCount,
                        isFavorite: false /*TODO*/
                    )
                )
                .ToList()
        );
    }
}

public class GetAccountPostsResponseDTO
{
    public List<PostBaseDTO> Posts { get; }

    public GetAccountPostsResponseDTO(List<PostBaseDTO> posts)
    {
        Posts = posts;
    }
}