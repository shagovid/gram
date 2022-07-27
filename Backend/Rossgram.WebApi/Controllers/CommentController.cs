using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rossgram.Application.Auth.Commands.SignUp;
using Rossgram.Application.Comments.Commands.AddComment;
using Rossgram.Application.Comments.Commands.LikeComment;
using Rossgram.Application.Comments.Commands.RemoveComment;
using Rossgram.Application.Comments.Commands.UnlikeComment;
using Rossgram.Application.Comments.Queries.GetComments;

namespace Rossgram.WebApi.Controllers;

public class CommentController : MediatorBaseController
{
    [HttpPost("add")]
    public Task<AddCommentResponseDTO> Add(
        [FromBody] AddCommentCommand query)
    {
        return Mediator.Send(query);
    }
        
    [HttpGet("get")]
    public Task<GetCommentsResponseDTO> Get(
        [FromQuery] GetCommentsQuery query)
    {
        return Mediator.Send(query);
    }
        
    [HttpDelete("remove")]
    public Task<RemoveCommentResponseDTO> Remove(
        [FromBody] RemoveCommentCommand query)
    {
        return Mediator.Send(query);
    }
        
    [HttpPatch("like")]
    public Task<LikeCommentResponseDTO> Like(
        [FromBody] LikeCommentCommand query)
    {
        return Mediator.Send(query);
    }
        
    [HttpPatch("unlike")]
    public Task<UnlikeCommentResponseDTO> Unlike(
        [FromBody] UnlikeCommentCommand query)
    {
        return Mediator.Send(query);
    }
}