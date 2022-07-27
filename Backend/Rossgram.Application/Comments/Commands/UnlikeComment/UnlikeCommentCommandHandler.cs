using System;
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

namespace Rossgram.Application.Comments.Commands.UnlikeComment;

public class UnlikeCommentCommandHandler
    : IRequestHandler<UnlikeCommentCommand, UnlikeCommentResponseDTO>
{
    private readonly ICurrentAuth _auth;
    private readonly IAppDbContext _context;

    public UnlikeCommentCommandHandler(
        ICurrentAuth auth,
        IAppDbContext context)
    {
        _auth = auth;
        _context = context;
    }

    public async Task<UnlikeCommentResponseDTO> Handle(
        UnlikeCommentCommand request,
        CancellationToken cancellationToken)
    {
        // Is liked?
        PostCommentLike? like = await _context.PostsCommentsLikes.FirstOrDefaultAsync(
            x => x.CommentId == request.Id && x.OwnerId == _auth.Id, cancellationToken);

        if (like is not null)
        {
            // Remove like
            _context.PostsCommentsLikes.Remove(like);
            await _context.SaveChangesAsync(cancellationToken);
        }
        
        // Return result
        return new UnlikeCommentResponseDTO();
    }
}

public class UnlikeCommentResponseDTO
{
    
}