using MediatR;
using Rossgram.Application.Auth.Commands.SignUp;
using Rossgram.Application.Common.Behaviours;

namespace Rossgram.Application.Accounts.Commands.UpdateAvatar;

[Authorize]
public class UpdateAvatarCommand
    : IRequest<UpdateAvatarResponseDTO>
{
    public long FileId { get; init; }
}