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

namespace Rossgram.Application.Accounts.Commands.Follow;

public class FollowCommandHandler
    : IRequestHandler<FollowCommand, FollowResponseDTO>
{
    private readonly ICurrentAuth _auth;
    private readonly IAppDbContext _context;

    public FollowCommandHandler(
        ICurrentAuth auth,
        IAppDbContext context)
    {
        _auth = auth;
        _context = context;
    }

    public async Task<FollowResponseDTO> Handle(
        FollowCommand request,
        CancellationToken cancellationToken)
    {
        // Deny self-follow
        if (request.AccountId == _auth.Id) throw new NotImplementedException();
            
        // Is already following?
        bool isFollowing = await _context.Followings.AnyAsync(
            x => x.AccountId == request.AccountId && x.FollowerId == _auth.Id, cancellationToken);
        
        if (!isFollowing)
        {
            // Create following
            Following following = new(
                id: default,
                accountId: request.AccountId,
                account: default!,
                followerId: _auth.Id,
                follower: default!);
            await _context.Followings.AddAsync(following, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        // Return result
        return new FollowResponseDTO();
    }
}

public class FollowResponseDTO
{
        
}