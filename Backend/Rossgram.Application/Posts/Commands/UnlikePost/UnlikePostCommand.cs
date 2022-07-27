using MediatR;
using Rossgram.Application.Common.Behaviours;

namespace Rossgram.Application.Posts.Commands.UnlikePost;

[Authorize]
public class UnlikePostCommand
    : IRequest<UnlikePostResponseDTO>
{
    public long Id { get; set; }
}