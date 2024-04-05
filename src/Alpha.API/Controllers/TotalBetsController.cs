using Alpha.API.Attributes;
using Alpha.API.Requests.TotalBets;
using Alpha.Reports.Models.TotalBets;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Alpha.API.Controllers;

[TypeFilter(typeof(MinimizeUMAttribute))]
public class TotalBetsController : ControllerBaseApi
{
    public TotalBetsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("Summary")]
    public Task<TotalBetsModel> Summary([FromQuery] TotalBetsQuery query)
        => ObjectAsync(query);

    [HttpGet("Details")]
    public Task<TotalBetsDetailsModel> Details([FromQuery] TotalBetsDetailsQuery query)
        => ObjectAsync(query);
}
