using MediatR;
using Rossgram.Application.Auth.Commands.SignUp;
using Rossgram.Application.Common.Behaviours;

namespace Rossgram.Application.Accounts.Commands.Unfollow;

[Authorize]
public class UnfollowCommand
    : IRequest<UnfollowResponseDTO>
{
    public long AccountId { get; init; }
}