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

namespace Rossgram.Application.Comments.Commands.RemoveComment;

public class RemoveCommentCommandHandler
    : IRequestHandler<RemoveCommentCommand, RemoveCommentResponseDTO>
{
    private readonly ICurrentAuth _auth;
    private readonly IAppDbContext _context;

    public RemoveCommentCommandHandler(
        ICurrentAuth auth,
        IAppDbContext context)
    {
        _auth = auth;
        _context = context;
    }

    public async Task<RemoveCommentResponseDTO> Handle(
        RemoveCommentCommand request,
        CancellationToken cancellationToken)
    {
        // Is liked?
        PostComment? comment = await _context.PostsComments.FirstOrDefaultAsync(
            x => x.Id == request.Id, cancellationToken);

        if (comment is not null)
        {
            if (comment.OwnerId != _auth.Id)
            {
                if (_auth.Role!.Permissions.NotContains(Permission.CanRemovePostComments))
                    throw new ForbiddenAccessError();
            }
            
            // Remove comment
            _context.PostsComments.Remove(comment);
            await _context.SaveChangesAsync(cancellationToken);
        }

        // Return result
        return new RemoveCommentResponseDTO();
    }
}

public class RemoveCommentResponseDTO
{
    
}