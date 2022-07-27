using MediatR;
using Rossgram.Application.Common.Behaviours;

namespace Rossgram.Application.Comments.Commands.RemoveComment;

[Authorize]
public class RemoveCommentCommand
    : IRequest<RemoveCommentResponseDTO>
{
    public long Id { get; set; }
}