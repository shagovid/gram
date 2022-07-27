using MediatR;
using Rossgram.Application.Common.Behaviours;

namespace Rossgram.Application.Comments.Commands.UnlikeComment;

[Authorize]
public class UnlikeCommentCommand
    : IRequest<UnlikeCommentResponseDTO>
{
    public long Id { get; set; }
}