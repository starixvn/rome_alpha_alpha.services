using Alpha.API.Attributes;
using Alpha.API.Requests.Reports.Outstanding;
using Alpha.Reports.Models.Outstanding;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Alpha.API.Controllers;

[TypeFilter(typeof(MinimizeUMFilter))]
public class OutstandingController : ControllerBaseApi
{
    public OutstandingController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("GetDownlineOutstanding")]
    public Task<OutstandingReport> GetDownlineOutstanding([FromQuery]GetDownlineOutstandingQuery query)
        => ObjectAsync(query);

    [HttpGet("GetTopUsername")]
    public Task<IEnumerable<string>> GetTopUsername([FromQuery]GetTopOutstandingUsernameQuery query)
        => ObjectAsync(query);

    [HttpGet("GetTopOutstanding")]
    public Task<OutstandingReport> GetTopOutstanding([FromQuery]GetTopOutstandingQuery query)
        => ObjectAsync(query);
}
