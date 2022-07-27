using MediatR;
using Rossgram.Application.Common.Behaviours;

namespace Rossgram.Application.Conversations.Commands.RemoveMessage;

[Authorize]
public class RemoveMessageCommand
    : IRequest<RemoveMessageResponseDTO>
{
    
}