using MediatR;
using Rossgram.Application.Auth.Commands.SignUp;
using Rossgram.Application.Common.Behaviours;

namespace Rossgram.Application.Accounts.Commands.Follow;

[Authorize]
public class FollowCommand
    : IRequest<FollowResponseDTO>
{
    public long AccountId { get; init; }
}