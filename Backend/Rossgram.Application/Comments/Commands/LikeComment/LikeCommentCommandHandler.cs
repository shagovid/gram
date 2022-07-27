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

namespace Rossgram.Application.Comments.Commands.LikeComment;

public class LikeCommentCommandHandler
    : IRequestHandler<LikeCommentCommand, LikeCommentResponseDTO>
{
    private readonly ICurrentAuth _auth;
    private readonly IAppDbContext _context;

    public LikeCommentCommandHandler(
        ICurrentAuth auth,
        IAppDbContext context)
    {
        _auth = auth;
        _context = context;
    }

    public async Task<LikeCommentResponseDTO> Handle(
        LikeCommentCommand request,
        CancellationToken cancellationToken)
    {
        // Is already liked?
        bool isLiked = await _context.PostsCommentsLikes.AnyAsync(
            x => x.CommentId == request.Id && x.OwnerId == _auth.Id, cancellationToken);

        if (!isLiked)
        {
            // Create like
            PostCommentLike like = new(
                id: default,
                commentId: request.Id,
                comment: default!,
                ownerId: _auth.Id,
                owner: default!);
            await _context.PostsCommentsLikes.AddAsync(like, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        
        // Return result
        return new LikeCommentResponseDTO();
    }
}

public class LikeCommentResponseDTO
{
    
}