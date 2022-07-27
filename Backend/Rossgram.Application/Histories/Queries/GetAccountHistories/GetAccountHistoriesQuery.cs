using MediatR;
using Rossgram.Application.Common.Behaviours;

namespace Rossgram.Application.Histories.Queries.GetAccountHistories;

[Authorize]
public class GetAccountHistoriesQuery
    : IRequest<GetAccountHistoriesResponseDTO>
{
    public long AccountId { get; set; }
}