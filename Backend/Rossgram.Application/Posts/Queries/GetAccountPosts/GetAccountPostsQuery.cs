using MediatR;

namespace Rossgram.Application.Posts.Queries.GetAccountPosts;

public class GetAccountPostsQuery
    : IRequest<GetAccountPostsResponseDTO>
{
    public long AccountId { get; set; }
    public int? Offset { get; set; }
    public int? Limit { get; set; }
}