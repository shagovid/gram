using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Rossgram.Application.Common.Interfaces;

namespace Rossgram.Application.Conversations.Commands.RemoveMessage;

public class RemoveMessageCommandHandler
    : IRequestHandler<RemoveMessageCommand, RemoveMessageResponseDTO>
{
    private readonly IAppDbContext _context;

    public RemoveMessageCommandHandler(
        IAppDbContext context)
    {
        _context = context;
    }

    public async Task<RemoveMessageResponseDTO> Handle(
        RemoveMessageCommand request,
        CancellationToken cancellationToken)
    {
        // Return result
        return new RemoveMessageResponseDTO();
    }
}

public class RemoveMessageResponseDTO
{
    
}