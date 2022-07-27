using MediatR;
using Rossgram.Domain.Enumerations;

namespace Rossgram.Application.Auth.Commands.SignUp;

public class SignUpCommand
    : IRequest<SignUpResponseDTO>
{
    public AccountDTO Account { get; set; } = null!;

    public string? NicknameKey { get; set; }
        
    public class AccountDTO
    {
        public Role Role { get; set; } = null!;
        public string Nickname { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Bio { get; set; } = null!;
        public bool IsVerified { get; set; }
    }
}