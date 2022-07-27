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

namespace Rossgram.Application.Posts.Commands.UnlikePost;

public class UnlikePostCommandHandler
    : IRequestHandler<UnlikePostCommand, UnlikePostResponseDTO>
{
    private readonly ICurrentAuth _auth;
    private readonly IAppDbContext _context;

    public UnlikePostCommandHandler(
        ICurrentAuth auth,
        IAppDbContext context)
    {
        _auth = auth;
        _context = context;
    }

    public async Task<UnlikePostResponseDTO> Handle(
        UnlikePostCommand request,
        CancellationToken cancellationToken)
    {
        // Is liked?
        PostLike? like = await _context.PostsLikes.FirstOrDefaultAsync(
            x => x.PostId == request.Id && x.OwnerId == _auth.Id, cancellationToken);

        if (like is not null)
        {
            // Remove like
            _context.PostsLikes.Remove(like);
            await _context.SaveChangesAsync(cancellationToken);
        }
        
        // Return result
        return new UnlikePostResponseDTO();
    }
}

public class UnlikePostResponseDTO
{
    
}