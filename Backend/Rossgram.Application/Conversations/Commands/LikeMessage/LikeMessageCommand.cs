using MediatR;
using Rossgram.Application.Common.Behaviours;

namespace Rossgram.Application.Conversations.Commands.LikeMessage;

[Authorize]
public class LikeMessageCommand
    : IRequest<LikeMessageResponseDTO>
{
    
}