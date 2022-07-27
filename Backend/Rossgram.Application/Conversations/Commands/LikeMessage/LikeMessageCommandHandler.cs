using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Rossgram.Application.Common.Interfaces;

namespace Rossgram.Application.Conversations.Commands.LikeMessage;

public class LikeMessageCommandHandler
    : IRequestHandler<LikeMessageCommand, LikeMessageResponseDTO>
{
    private readonly IAppDbContext _context;

    public LikeMessageCommandHandler(
        IAppDbContext context)
    {
        _context = context;
    }

    public async Task<LikeMessageResponseDTO> Handle(
        LikeMessageCommand request,
        CancellationToken cancellationToken)
    {
        // Return result
        return new LikeMessageResponseDTO();
    }
}

public class LikeMessageResponseDTO
{
    
}