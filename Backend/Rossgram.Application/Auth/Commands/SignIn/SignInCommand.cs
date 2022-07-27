using MediatR;

namespace Rossgram.Application.Auth.Commands.SignIn;

public class SignInCommand
    : IRequest<SignInResponseDTO>
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
}