using MediatR;
using Rossgram.Application.Common.Behaviours;

namespace Rossgram.Application.Posts.Commands.LikePost;

[Authorize]
public class LikePostCommand
    : IRequest<LikePostResponseDTO>
{
    public long Id { get; set; }
}