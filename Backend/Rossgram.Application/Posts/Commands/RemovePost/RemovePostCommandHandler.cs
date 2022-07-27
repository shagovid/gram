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

namespace Rossgram.Application.Posts.Commands.RemovePost;

public class RemovePostCommandHandler
    : IRequestHandler<RemovePostCommand, RemovePostResponseDTO>
{
    private readonly ICurrentAuth _auth;
    private readonly IAppDbContext _context;

    public RemovePostCommandHandler(
        ICurrentAuth auth,
        IAppDbContext context)
    {
        _auth = auth;
        _context = context;
    }

    public async Task<RemovePostResponseDTO> Handle(
        RemovePostCommand request,
        CancellationToken cancellationToken)
    {
        // Is liked?
        Post? post = await _context.Posts.FirstOrDefaultAsync(
            x => x.Id == request.Id, cancellationToken);

        if (post is not null)
        {
            if (post.OwnerId != _auth.Id)
            {
                if (_auth.Role!.Permissions.NotContains(Permission.CanRemovePosts))
                    throw new ForbiddenAccessError();
            }
            
            // Remove comment
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync(cancellationToken);
        }

        // Return result
        return new RemovePostResponseDTO();
    }
}

public class RemovePostResponseDTO
{
    
}