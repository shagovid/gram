using MediatR;

namespace Rossgram.Application.Accounts.Queries.GetAccount;

public class GetAccountQuery
    : IRequest<GetAccountResponseDTO>
{
    public long AccountId { get; set; }
}