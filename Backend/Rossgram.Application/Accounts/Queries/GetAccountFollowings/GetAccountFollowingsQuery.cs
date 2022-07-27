using MediatR;

namespace Rossgram.Application.Accounts.Queries.GetAccountFollowings;

public class GetAccountFollowingsQuery
    : IRequest<GetAccountFollowingsResponseDTO>
{
    public long AccountId { get; set; }
    public int? Offset { get; set; }
    public int? Limit { get; set; }
}