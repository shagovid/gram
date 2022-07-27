using MediatR;
using Rossgram.Application.Common.Behaviours;

namespace Rossgram.Application.Conversations.Queries.GetConversations;

[Authorize]
public class GetConversationsQuery
    : IRequest<GetConversationsResponseDTO>
{
    public int? Offset { get; set; }
    public int? Limit { get; set; }
}