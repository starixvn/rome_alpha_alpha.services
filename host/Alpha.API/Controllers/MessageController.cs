using Alpha.API.Requests.Notifications;
using Alpha.Notifications.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Alpha.API.Controllers;

public class MessageController : ControllerBaseApi
{
    public MessageController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("GetMessages")]
    public Task<IEnumerable<Message>> GetMessages([FromQuery]GetMessageQuery query)
        => ObjectAsync(query);
}
