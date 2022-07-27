using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rossgram.Application.Conversations.Commands.SendMessage;
using Rossgram.Application.Conversations.Queries.GetConversations;
using Rossgram.Application.Conversations.Queries.GetMessages;

namespace Rossgram.WebApi.Controllers;

public class ConversationController : MediatorBaseController
{
    [HttpGet("get")]
    public Task<GetConversationsResponseDTO> GetDialogs(
        [FromQuery] GetConversationsQuery request)
    {
        return Mediator.Send(request);
    }
    
    [HttpPost("send-message")]
    public Task<SendMessageResponseDTO> Send(
        [FromBody] SendMessageCommand request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("get-messages")]
    public Task<GetMessagesResponseDTO> Get(
        [FromQuery] GetMessagesQuery request)
    {
        return Mediator.Send(request);
    }
}