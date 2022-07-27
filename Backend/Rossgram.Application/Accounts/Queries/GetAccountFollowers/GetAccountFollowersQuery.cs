using MediatR;

namespace Rossgram.Application.Accounts.Queries.GetAccountFollowers;

public class GetAccountFollowersQuery
    : IRequest<GetAccountFollowersResponseDTO>
{
    public long AccountId { get; set; }
    public int? Offset { get; set; }
    public int? Limit { get; set; }
}