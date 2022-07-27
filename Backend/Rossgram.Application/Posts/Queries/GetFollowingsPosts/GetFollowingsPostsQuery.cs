using MediatR;
using Rossgram.Application.Common.Behaviours;

namespace Rossgram.Application.Posts.Queries.GetFollowingsPosts;

[Authorize]
public class GetFollowingsPostsQuery
    : IRequest<GetFollowingsPostsResponseDTO>
{
    public int? Offset { get; set; }
    public int? Limit { get; set; }
}