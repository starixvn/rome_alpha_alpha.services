using Alpha.API.Attributes;
using Alpha.API.Requests.Reports.Outstanding;
using Alpha.Reports.Models.Outstanding;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Alpha.API.Controllers;

//[TypeFilter(typeof(SubAccPermissionAuthorization))]
public class OutstandingController : ControllerBaseApi
{
    public OutstandingController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("GetMyOutstanding")]
    public Task<MyOutstanding> GetMyOutstanding([FromQuery]GetMyOutstandingQuery query)
        => ObjectAsync(query);

    [HttpGet("GetDownlineOutstanding")]
    public Task<IEnumerable<DownlineOutstanding>> GetDownlineOutstanding([FromQuery]GetDownlineOutstandingQuery query)
        => ObjectAsync(query);

    [HttpGet("GetTopUsername")]
    public Task<IEnumerable<string>> GetTopUsername([FromQuery]GetTopOutstandingUsernameQuery query)
        => ObjectAsync(query);

    [HttpGet("GetTopOutstanding")]
    public Task<TopOutstanding> GetTopOutstanding([FromQuery]GetTopOutstandingQuery query)
        => ObjectAsync(query);
}
