using MediatR;
using Rossgram.Application.Common.Behaviours;

namespace Rossgram.Application.Conversations.Commands.EditMessage;

[Authorize]
public class EditMessageCommand
    : IRequest<EditMessageResponseDTO>
{
    
}