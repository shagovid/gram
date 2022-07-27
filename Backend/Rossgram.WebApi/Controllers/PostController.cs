using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rossgram.Application.Auth.Commands.SignUp;
using Rossgram.Application.Posts.Commands.LikePost;
using Rossgram.Application.Posts.Commands.RemovePost;
using Rossgram.Application.Posts.Commands.UnlikePost;
using Rossgram.Application.Posts.Commands.UploadPost;
using Rossgram.Application.Posts.Queries.GetAccountPosts;
using Rossgram.Application.Posts.Queries.GetFollowingsPosts;

namespace Rossgram.WebApi.Controllers;

public class PostController : MediatorBaseController
{
    [HttpPost("upload")]
    public Task<UploadPostResponseDTO> Upload(
        [FromBody] UploadPostCommand query)
    {
        return Mediator.Send(query);
    }
    
    [HttpGet("get-by-account")]
    public Task<GetAccountPostsResponseDTO> Get(
        [FromQuery] GetAccountPostsQuery query)
    {
        return Mediator.Send(query);
    }
        
    [HttpGet("get-by-followings")]
    public Task<GetFollowingsPostsResponseDTO> Remove(
        [FromQuery] GetFollowingsPostsQuery query)
    {
        return Mediator.Send(query);
    }
        
    [HttpDelete("remove")]
    public Task<RemovePostResponseDTO> Remove(
        [FromQuery] RemovePostCommand query)
    {
        return Mediator.Send(query);
    }
        
    [HttpPatch("like")]
    public Task<LikePostResponseDTO> Like(
        [FromBody] LikePostCommand query)
    {
        return Mediator.Send(query);
    }
        
    [HttpPatch("unlike")]
    public Task<UnlikePostResponseDTO> Unlike(
        [FromBody] UnlikePostCommand query)
    {
        return Mediator.Send(query);
    }
}