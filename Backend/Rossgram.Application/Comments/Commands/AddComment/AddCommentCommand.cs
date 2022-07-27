using MediatR;
using Rossgram.Application.Common.Behaviours;

namespace Rossgram.Application.Comments.Commands.AddComment;

[Authorize]
public class AddCommentCommand
    : IRequest<AddCommentResponseDTO>
{
    public long PostId { get; set; }
    public string Text { get; set; }
}