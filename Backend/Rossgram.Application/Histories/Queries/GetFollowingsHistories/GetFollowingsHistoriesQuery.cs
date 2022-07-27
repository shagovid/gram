using MediatR;
using Rossgram.Application.Common.Behaviours;

namespace Rossgram.Application.Histories.Queries.GetFollowingsHistories;

[Authorize]
public class GetFollowingsHistoriesQuery
    : IRequest<GetFollowingsHistoriesResponseDTO>
{
    public int? Offset { get; set; }
    public int? Limit { get; set; }
}