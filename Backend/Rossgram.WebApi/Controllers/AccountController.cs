using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rossgram.Application.Accounts.Commands.Follow;
using Rossgram.Application.Accounts.Commands.Unfollow;
using Rossgram.Application.Accounts.Queries.GetAccount;
using Rossgram.Application.Accounts.Queries.GetAccountFollowers;
using Rossgram.Application.Accounts.Queries.GetAccountFollowings;

namespace Rossgram.WebApi.Controllers;

public class AccountController : MediatorBaseController
{
    [HttpGet("get")]
    public Task<GetAccountResponseDTO> Get(
        [FromQuery] GetAccountQuery request)
    {
        return Mediator.Send(request);
    }
        
    [HttpPatch("follow")]
    public Task<FollowResponseDTO> Follow(
        [FromBody] FollowCommand request)
    {
        return Mediator.Send(request);
    }
        
    [HttpPatch("unfollow")]
    public Task<UnfollowResponseDTO> Unfollow(
        [FromBody] UnfollowCommand request)
    {
        return Mediator.Send(request);
    }
    
    [HttpGet("get-followers")]
    public Task<GetAccountFollowersResponseDTO> Follow(
        [FromQuery] GetAccountFollowersQuery request)
    {
        return Mediator.Send(request);
    }
    
    [HttpGet("get-followings")]
    public Task<GetAccountFollowingsResponseDTO> Follow(
        [FromQuery] GetAccountFollowingsQuery request)
    {
        return Mediator.Send(request);
    }
}