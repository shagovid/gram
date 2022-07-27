using MediatR;
using Rossgram.Application.Common.Behaviours;

namespace Rossgram.Application.Posts.Commands.RemovePost;

[Authorize]
public class RemovePostCommand
    : IRequest<RemovePostResponseDTO>
{
    public long Id { get; set; }
}