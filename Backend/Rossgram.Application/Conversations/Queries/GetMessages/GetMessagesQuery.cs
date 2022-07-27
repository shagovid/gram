using MediatR;
using Rossgram.Application.Common.Behaviours;

namespace Rossgram.Application.Conversations.Queries.GetMessages;

[Authorize]
public class GetMessagesQuery
    : IRequest<GetMessagesResponseDTO>
{
    public long? RecipientId { get; set; }
    public long? ConversationId { get; set; }
    public int? Offset { get; set; }
    public int? Limit { get; set; }
}