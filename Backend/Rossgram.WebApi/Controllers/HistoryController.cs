using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rossgram.Application.Auth.Commands.SignUp;
using Rossgram.Application.Histories.Commands.UploadHistory;
using Rossgram.Application.Histories.Queries.GetAccountHistories;
using Rossgram.Application.Histories.Queries.GetFollowingsHistories;

namespace Rossgram.WebApi.Controllers;

public class HistoryController : MediatorBaseController
{
    [HttpPost("upload")]
    public Task<UploadHistoryResponseDTO> Upload(
        [FromBody] UploadHistoryCommand query)
    {
        return Mediator.Send(query);
    }
        
    [HttpGet("get-by-account")]
    public Task<GetAccountHistoriesResponseDTO> Get(
        [FromQuery] GetAccountHistoriesQuery query)
    {
        return Mediator.Send(query);
    }
        
    [HttpGet("get-by-followings")]
    public Task<GetFollowingsHistoriesResponseDTO> Remove(
        [FromQuery] GetFollowingsHistoriesQuery query)
    {
        return Mediator.Send(query);
    }
}