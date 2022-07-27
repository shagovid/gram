using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rossgram.Application.Auth.Commands.SignIn;
using Rossgram.Application.Auth.Commands.SignUp;

namespace Rossgram.WebApi.Controllers;

public class AuthController : MediatorBaseController
{
    [HttpPost("sign-up")]
    public Task<SignUpResponseDTO> SignUp(
        [FromBody] SignUpCommand request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("sign-in")]
    public Task<SignInResponseDTO> SignIn(
        [FromQuery] SignInCommand request)
    {
        return Mediator.Send(request);
    }
}