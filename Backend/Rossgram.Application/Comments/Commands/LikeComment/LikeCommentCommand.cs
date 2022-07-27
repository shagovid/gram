using MediatR;
using Rossgram.Application.Common.Behaviours;

namespace Rossgram.Application.Comments.Commands.LikeComment;

[Authorize]
public class LikeCommentCommand
    : IRequest<LikeCommentResponseDTO>
{
    public long Id { get; set; }
}