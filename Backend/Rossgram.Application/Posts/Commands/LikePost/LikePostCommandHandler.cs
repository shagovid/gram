using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rossgram.Application.Auth.Commands.SignUp;
using Rossgram.Application.Common.Interfaces;
using Rossgram.Application.Common.Services;
using Rossgram.Domain;
using Rossgram.Domain.Entities;
using Rossgram.Domain.Entities.Posts;
using Rossgram.Domain.Enumerations;
using Rossgram.Domain.Errors.Access;

namespace Rossgram.Application.Posts.Commands.LikePost;

public class LikePostCommandHandler
    : IRequestHandler<LikePostCommand, LikePostResponseDTO>
{
    private readonly ICurrentAuth _auth;
    private readonly IAppDbContext _context;

    public LikePostCommandHandler(
        ICurrentAuth auth,
        IAppDbContext context)
    {
        _auth = auth;
        _context = context;
    }

    public async Task<LikePostResponseDTO> Handle(
        LikePostCommand request,
        CancellationToken cancellationToken)
    {
        // Is already liked?
        bool isLiked = await _context.PostsLikes.AnyAsync(
            x => x.PostId == request.Id && x.OwnerId == _auth.Id, cancellationToken);

        if (!isLiked)
        {
            // Create like
            PostLike like = new(
                id: default,
                postId: request.Id,
                post: default!,
                ownerId: _auth.Id,
                owner: default!);
            await _context.PostsLikes.AddAsync(like, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        
        // Return result
        return new LikePostResponseDTO();
    }
}

public class LikePostResponseDTO
{
    
}