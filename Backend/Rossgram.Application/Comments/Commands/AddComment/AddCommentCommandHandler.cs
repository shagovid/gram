using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Rossgram.Application.Auth.Commands.SignUp;
using Rossgram.Application.Common.Interfaces;
using Rossgram.Application.Common.Services;
using Rossgram.Domain;
using Rossgram.Domain.Entities;
using Rossgram.Domain.Entities.Posts;
using Rossgram.Domain.Enumerations;
using Rossgram.Domain.Errors.Access;

namespace Rossgram.Application.Comments.Commands.AddComment;

public class AddCommentCommandHandler
    : IRequestHandler<AddCommentCommand, AddCommentResponseDTO>
{
    private readonly ICurrentAuth _auth;
    private readonly ITimeService _time;
    private readonly CommentController _commentController;
    private readonly IAppDbContext _context;

    public AddCommentCommandHandler(
        ICurrentAuth auth,
        ITimeService time,
        CommentController commentController,
        IAppDbContext context)
    {
        _auth = auth;
        _time = time;
        _commentController = commentController;
        _context = context;
    }

    public async Task<AddCommentResponseDTO> Handle(
        AddCommentCommand request,
        CancellationToken cancellationToken)
    {
        PostComment comment = new PostComment(
            id: default,
            ownerId: _auth.Id,
            owner: default!,
            postId: request.PostId,
            post: default!,
            timeStamp: _time.Now,
            text: _commentController.Validate(request.Text),
            likes: default!
        );

        await _context.PostsComments.AddAsync(comment, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        // Return result
        return new AddCommentResponseDTO();
    }
}

public class AddCommentResponseDTO
{
    
}