using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rossgram.Application.Auth.Commands.SignUp;
using Rossgram.Application.Common.Interfaces;
using Rossgram.Domain;
using Rossgram.Domain.Entities;
using Rossgram.Domain.Enumerations;
using Rossgram.Domain.Errors.Access;

namespace Rossgram.Application.Accounts.Commands.Unfollow;

public class UnfollowCommandHandler
    : IRequestHandler<UnfollowCommand, UnfollowResponseDTO>
{
    private readonly ICurrentAuth _auth;
    private readonly IAppDbContext _context;

    public UnfollowCommandHandler(
        ICurrentAuth auth,
        IAppDbContext context)
    {
        _auth = auth;
        _context = context;
    }

    public async Task<UnfollowResponseDTO> Handle(
        UnfollowCommand request,
        CancellationToken cancellationToken)
    {
        // Is following?
        Following? following = await _context.Followings.FirstOrDefaultAsync(
            x => x.AccountId == request.AccountId && x.FollowerId == _auth.Id, cancellationToken);

        if (following is not null)
        {
            // Remove following
            _context.Followings.Remove(following);
            await _context.SaveChangesAsync(cancellationToken);
        }

        // Return result
        return new UnfollowResponseDTO();
    }
}

public class UnfollowResponseDTO
{
        
}