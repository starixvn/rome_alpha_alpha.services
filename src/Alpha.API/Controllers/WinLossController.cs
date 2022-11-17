using Alpha.API.Attributes;
using Alpha.API.Requests.Reports.WinLoss;
using Alpha.Reports.Models.WinLoss;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Alpha.API.Controllers;

[TypeFilter(typeof(MinimizeUMAttribute))]
public class WinLossController : ControllerBaseApi
{
    public WinLossController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("GetWinLoss")]
    public Task<WinLossBase> GetWinLoss([FromQuery] GetWinLossQuery query)
        => ObjectAsync(query);

    [HttpGet("GetDownlineWinLoss")]
    public Task<WinLossBase> GetDownlineWinLoss([FromQuery] GetDownlineWinLossQuery query)
        => ObjectAsync(query);

    [HttpGet("GetTopUsername")]
    public Task<IEnumerable<string>> GetTopUsername([FromQuery] GetTopUsernameWinLossQuery query)
        => ObjectAsync(query);

    [HttpGet("EnteredResult/Yesterday")]
    public Task<EnteredResult> GetEnteredResult()
        => ObjectAsync(new GetEnteredResultQuery());
}
