using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Rossgram.Application.Common.Interfaces;

namespace Rossgram.Application.Conversations.Commands.EditMessage;

public class EditMessageCommandHandler
    : IRequestHandler<EditMessageCommand, EditMessageResponseDTO>
{
    private readonly IAppDbContext _context;

    public EditMessageCommandHandler(
        IAppDbContext context)
    {
        _context = context;
    }

    public async Task<EditMessageResponseDTO> Handle(
        EditMessageCommand request,
        CancellationToken cancellationToken)
    {
        
        // Return result
        return new EditMessageResponseDTO();
    }
}

public class EditMessageResponseDTO
{
    
}